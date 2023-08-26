using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ShipHyperdrive : ShipSystem
{
    public bool isWarping;
    private bool willExplode;
    
    [Header("Hyperdrive Settings")]
    [SerializeField] private int numOfWarps = 0;
    [SerializeField] private float timeInHyperspace = 3f;
    [SerializeField] private float minSafetyDistance = 10f;
    [SerializeField] private float gravitySafeDistance = 50f;
    [SerializeField] private float hyperdriveCooldown = 30f;
    [SerializeField] private LayerMask avoidLayers;

    private float timeTillCanWarp;

    [Header("Explosion Settings")]
    [SerializeField, Range(0f, 1f)] private float explosionProbability;
    [SerializeField, Range(0f, 0.1f)] private float probabilityIncrease;
    
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
        timeTillCanWarp -= Time.deltaTime;
    }

    private void OnHyperdriveInput()
    {
        if (timeTillCanWarp <= 0 && !isWarping)
        {
            StartCoroutine(EnterHyperspace());
        }
    }

    public IEnumerator EnterHyperspace()
    {
        isWarping = true;
        ship.id.Events.OnHyperdriveActivated?.Invoke();
        
        // Teleport to Hyperspace Region
        transform.position = ship.id.HyperspaceLocation;
        Debug.Log($"Entered Hyperspace: Ship Postition: {transform.position}");
        
        yield return new WaitForSeconds(timeInHyperspace);

        // Check if will explode on Reentry
        willExplode = CheckIfExplode();

        numOfWarps++;

        // Teleport
        TeleportRandom();

        Debug.Log($"Warped to {transform.position}");

        timeTillCanWarp = hyperdriveCooldown;

        if (willExplode)
        {
            ship.id.Events.OnTakeDamage?.Invoke(100); 
            Debug.Log("Exploded");
        }
        else
        {
            explosionProbability += probabilityIncrease * numOfWarps;
            Debug.Log("Not Exploded");
        }

        isWarping = false;
    }

    public void TeleportRandom()
    { 
        Vector3 position = Vector3.zero;

        for (int i = 0; i < 50; i++)
        {
            position = Random.insideUnitSphere * FieldManager.Instance.Radius;
            Debug.Log($"{i}: Teleport to {position} is safe: {TeleportLocationSafe(position)}");

            if (TeleportLocationSafe(position)) break;
        }
        
        transform.position = position;
    }

    private bool TeleportLocationSafe(Vector3 position)
    {
        Collider[] bodies = Physics.OverlapSphere(position, minSafetyDistance, avoidLayers);

        if (bodies.Length > 0)
            return false;

        bodies.AddRange(Physics.OverlapSphere(position, gravitySafeDistance, avoidLayers));
        
        if (bodies.Length == 0)
            return true;
        
        foreach (Collider b in bodies)
        {
            GravityBody body = b.transform.root.GetComponent<GravityBody>();

            if (body.bodyType == BodyTypes.star || body.bodyType == BodyTypes.planet)
                return false;
        } 
        
        return true;
    }

    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, minSafetyDistance);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, gravitySafeDistance);
    }

    private bool CheckIfExplode()
    {
        return Random.value < explosionProbability;
    }
}
