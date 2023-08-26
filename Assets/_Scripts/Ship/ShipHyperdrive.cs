using System.Collections;
using UnityEngine;

public class ShipHyperdrive : ShipSystem
{
    Rigidbody rb;

    public bool isWarping;
    private bool willExplode;
    
    [Header("Hyperdrive Settings")]
    [SerializeField] private int numOfWarps = 0;
    [SerializeField] private float timeInHyperspace = 3f;
    [SerializeField] private float minSafetyDistance = 10f;
    [SerializeField] private float gravitySafeDistance = 50f;
    [SerializeField] private float hyperdriveCooldown = 30f;
    [SerializeField] private LayerMask avoidLayers;
    [SerializeField] private Vector3 hyperspace;

    private float timeTillCanWarp;

    [Header("Explosion Settings")]
    [SerializeField, Range(0f, 1f)] private float explosionProbability;
    [SerializeField, Range(0f, 0.1f)] private float probabilityIncrease;

    protected override void Awake() 
    {
        base.Awake();
        
        hyperspace = ship.id.HyperspaceLocation;
        rb = GetComponent<Rigidbody>();
    }
    
    private void OnEnable() 
    {
        ship.id.Events.OnHyperdriveInput += OnHyperdriveInput;
    }

    private void OnDisable() 
    {
        ship.id.Events.OnHyperdriveInput -= OnHyperdriveInput;
    }

    private void Update() 
    {
        if (!isWarping)
            timeTillCanWarp -= Time.deltaTime;
    }

    private void OnHyperdriveInput()
    {
        if (timeTillCanWarp <= 0 && !isWarping)
        {
            StartCoroutine(TeleportRandom());
        }
    }

    private void EnterHyperspace()
    {
        Debug.Log("Entered Hyperspace");
        // Teleport to Hyperspace Region
        rb.position = hyperspace;
    }

    private IEnumerator TeleportRandom()
    { 
        Debug.Log($"Commencing Warp");
        isWarping = true;

        ship.id.Events.OnHyperdriveActivated?.Invoke();
        
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
        
        EnterHyperspace();

        yield return new WaitForSeconds(timeInHyperspace);
        
        Debug.Log("Exited Hyperspace");
        
        willExplode = CheckIfExplode(); // Check if will explode on Reentry

        Vector3 position = CalculateRandomPosition();

        numOfWarps++;
        
        Debug.Log("Exited Hyperspace");

        Teleport(position);
        
        Debug.Log($"Teleported to position: {position}");

        // Explosive
        if (willExplode)
        {
            ship.id.Events.OnTakeDamage?.Invoke(100); 
            ship.id.Events.OnShipExplode?.Invoke();
            Debug.Log("Exploded");
        }
        else
        {
            explosionProbability += probabilityIncrease * numOfWarps;
            Debug.Log("Not Exploded");
        } 

        rb.isKinematic = false;
        isWarping = false;
        timeTillCanWarp = hyperdriveCooldown;
    }

    public void FieldTeleport(Vector3 position, Vector3 vel)
    {
        StartCoroutine(TeleportToPosition(position, vel));
    }

    private IEnumerator TeleportToPosition(Vector3 position, Vector3 velocity)
    {
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
        
        EnterHyperspace();

        yield return new WaitForSeconds(timeInHyperspace);
        
        Debug.Log("Exited Hyperspace");
        
        Teleport(position);
        rb.isKinematic = false;
        rb.velocity = velocity;
    }

    private void Teleport(Vector3 position)
    {
        rb.position = position;
    }

    private bool TeleportLocationSafe(Vector3 position)
    {
        Collider[] bodies = Physics.OverlapSphere(position, gravitySafeDistance, avoidLayers);

        if (bodies.Length == 0)
            return true;

        foreach (Collider b in bodies)
        {
            GravityBody body = b.transform.root.GetComponent<GravityBody>();

            if (Vector3.Distance(position, body.transform.position) < minSafetyDistance)
                return false;

            if (body.bodyType == BodyTypes.star || body.bodyType == BodyTypes.planet)
                return false;
        } 
        
        return true;
    }

    private Vector3 CalculateRandomPosition()
    {
        Vector3 position = Vector3.zero;
        
        for (int i = 0; i < 25; i++)
        {
            position = Random.insideUnitSphere * FieldManager.Instance.Radius;

            bool isSafe = TeleportLocationSafe(position);
            Debug.Log($"{position} is safe: {isSafe}");

            if (isSafe) break;
        }  

        return position;
    }

    private bool CheckIfExplode()
    {
        return Random.value < explosionProbability;
    }

    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, minSafetyDistance);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, gravitySafeDistance);
    }
}
