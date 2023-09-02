using System.Collections.Generic;
using UnityEngine;
using QFSW.QC;

public class UniverseSimManager : Singleton<UniverseSimManager>
{
    private FieldManager field; 
    public GravityBody GravityCentre;
    [field:SerializeField] public Universe universalValues { get; private set; }
    
    [SerializeField] List<GravityBody> bodies = new List<GravityBody>();
    [SerializeField] private GameObject[] asteroidPrefabs; 

    [Range(0, 150)] public int numberOfAsteroids;
    public float maxSpawnRadius;  

    protected override void Awake() 
    {
        base.Awake();

        field = GetComponent<FieldManager>();
        maxSpawnRadius = field.Radius;

        foreach (var body in bodies)
        {
            if (body.bodyType == BodyTypes.star)
                GravityCentre = body;
            
            break;
        }
        
        SetupBodiesList();
    }

    private void OnValidate() 
    {
        SetupBodiesList();
        field = GetComponent<FieldManager>();

        maxSpawnRadius = field.Radius;

        foreach (var body in bodies)
        {
            if (body.bodyType == BodyTypes.star)
                GravityCentre = body;
            
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
            Vector3 randomPosition = Random.insideUnitSphere * maxSpawnRadius;
            Quaternion randomRotation = Quaternion.Euler(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));

            float randomRadius = Random.Range(25f, 200f);
            float randomSpeed = Random.Range(15f, 45f);
            float randomGravity = Random.Range(0.05f, 1f);

            SpawnAsteroid(randomPosition, randomRotation, randomSpeed, randomRadius, randomGravity);
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
    public void SpawnAsteroid(Vector3 position, Quaternion rotation, float initialSpeed, float radius = 10f, float surfaceGrav = 5f)
    {
        var newBody = Instantiate(asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)], position, rotation, transform).GetComponent<GravityBody>();
        newBody.transform.root.parent = transform;

        newBody.InitAsteroid(radius, surfaceGrav, initialSpeed);

        AddNewBody(newBody);
    }

}
