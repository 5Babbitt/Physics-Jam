using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(TrailRenderer))]
public class GravityBody : MonoBehaviour
{
    Rigidbody rb;
    
    public Rigidbody Rigidbody { get { return rb; } }
    

    [Header("General Properties")]
    [Range(1f, 200f)] public float radius = 5f;
    public float surfaceGravity = 1;
    public Vector3 initialVelocity;

    private Vector3 currentVelocity;
    private Vector3 currentForce;

    [SerializeField] private Transform meshHolder;
    
    public BodyTypes bodyType;

    [Header("Ship Porperties")]
    public float mass;

    private void Awake()
    {
        currentVelocity = initialVelocity;
        
        SetupRigidbody();

        rb.useGravity = false;
        rb.velocity = currentVelocity;
    }

    public void UpdateForce(GravityBody[] allBodies)
    {
        Vector3 cumulativeForce = Vector3.zero;
        
        foreach (var body in allBodies)
        {
            if (body != this)
            {
                float sqrDistance = (body.Rigidbody.position - rb.position).sqrMagnitude;
                Vector3 forceDir = (body.Rigidbody.position - rb.position).normalized;

                Vector3 force = forceDir * Universe.gravitationalConstant * mass * body.mass / sqrDistance;

                cumulativeForce += force;
            }

            currentForce = cumulativeForce;
        }
    }

    public void ApplyForce()
    {
        rb.AddForce(currentForce);
    }

    private void OnValidate() 
    {
        CalculateMass();
        SetupRigidbody();

        if (meshHolder != null && bodyType != BodyTypes.ship)
            meshHolder.localScale = Vector3.one * radius * 2;
    }

    private void CalculateMass()
    {
        if (bodyType == BodyTypes.ship)
            return;
        
        mass = surfaceGravity * radius * radius / Universe.gravitationalConstant;
    }

    private void SetupRigidbody()
    {
        rb = GetComponent<Rigidbody>();
        rb.mass = mass;
    }

    private void OnDestroy() 
    {
        Universe.OnGravityBodyDestroyed?.Invoke(this);
    }

    private void OnDrawGizmosSelected() 
    {
        
    }
}

public enum BodyTypes
{
    star,
    planet,
    asteroid,
    ship
}
