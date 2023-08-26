using System.Collections.Generic;
using UnityEngine;
using QFSW.QC;

public class UniverseSimManager : Singleton<UniverseSimManager>
{
    private FieldManager field; 
    
    [SerializeField] List<GravityBody> bodies = new List<GravityBody>();
    [field:SerializeField] public Universe universalValues { get; private set; }

    public GravityBody Star;

    [SerializeField] private GameObject[] asteroidPrefabs; 

    public int numberOfAsteroids;
    public float asteroidSpawnRadius;  

    protected override void Awake() 
    {
        base.Awake();

        foreach (var body in bodies)
        {
            if (body.bodyType == BodyTypes.star)
                Star = body;
            
            break;
        }
        
        SetupBodiesList();
    }

    private void OnValidate() 
    {
        SetupBodiesList();

        foreach (var body in bodies)
        {
            if (body.bodyType == BodyTypes.star)
                Star = body;
            
            break;
        }
    }

    private void Start()
    {
        SpawnAsteroids();
    }

    private void SpawnAsteroids()
    {
        for (int i = 0; i < numberOfAsteroids; i++)
        {
            Vector3 randomPosition = Random.insideUnitSphere * asteroidSpawnRadius;

            Quaternion randomRotation = Quaternion.Euler(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));

            float randomRadius = Random.Range(0.25f, 2.5f);
            //GameObject spawnedItem = Instantiate(selectedPrefab, randomPosition, randomRotation);

            SpawnAsteroid(randomPosition, randomRotation, Random.Range(50f, 75f), randomRadius);
        }
    }

    private void OnEnable() 
    {
        Universe.OnGravityBodyDestroyed += OnGravityBodyDestroyed;
    }

    private void OnDisable() 
    {
        Universe.OnGravityBodyDestroyed -= OnGravityBodyDestroyed;
    }

    private void FixedUpdate() 
    {
        foreach (var body in bodies)
        {
            body.UpdateForce(bodies.ToArray());
        }

        foreach (var body in bodies)
        {
            body.ApplyForce();
        }
        
    }

    public static List<GravityBody> Bodies
    {
        get { return Instance.bodies; }
    }
    
    public void AddNewBody(GravityBody body)
    {
        var newBody = body;
        
        bodies.Add(newBody);
    }

    public void OnGravityBodyDestroyed(GravityBody body)
    {
        var newBody = body;
        
        bodies.Remove(newBody);
    }

    private void SetupBodiesList()
    {
        bodies.Clear();

        GravityBody[] foundBodies = FindObjectsOfType<GravityBody>();

        bodies.AddRange(foundBodies);
    }

    [Command]
    public void SpawnAsteroid(Vector3 position, Quaternion rotation, float initialSpeed, float radius = 1.5f, float surfaceGrav = 5f)
    {
        var newBody = Instantiate(asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)], position, rotation, this.transform).GetComponent<GravityBody>();
        newBody.transform.root.parent = transform;

        newBody.InitAsteroid(radius, surfaceGrav, initialSpeed);

        AddNewBody(newBody);
    }

}
