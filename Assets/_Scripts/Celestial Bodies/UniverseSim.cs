using System.Collections.Generic;
using UnityEngine;
using QFSW.QC;

public class UniverseSim : Singleton<UniverseSim>
{
    [SerializeField] List<GravityBody> bodies = new List<GravityBody>();
    [field:SerializeField] public Universe universalValues { get; private set; }

    [SerializeField] private GameObject gravityBodyPrefab; 

    protected override void Awake() 
    {
        SetupBodiesList();
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
    public void SpawnGravityObject(float xPos = 25, float zPos = 0, float velX = 0f, float velZ = 7f, float mass = 10f)
    {
        var newBody = Instantiate(gravityBodyPrefab, new Vector3(xPos, 0, zPos), Quaternion.identity).GetComponent<GravityBody>();
        newBody.mass = mass;
        var vel = new Vector3(velX, 0, velZ);

        PhysicsHelper.ApplyForceToReachVelocity(newBody.Rigidbody, vel, newBody.mass, ForceMode.Impulse);

        AddNewBody(newBody);
    }

}
