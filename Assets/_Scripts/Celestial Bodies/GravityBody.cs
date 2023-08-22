using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GravityBody : MonoBehaviour
{
    public float mass;
    public Vector3 initialVelocity;

    private Vector3 currentVelocity;
    private Vector3 currentForce;

    Rigidbody rb;
    public Rigidbody Rigidbody { get { return rb; } }

    private void Awake()
    {
        SetupRigidbody();

        currentVelocity = initialVelocity;
        PhysicsHelper.ApplyForceToReachVelocity(rb, currentVelocity, mass, ForceMode.Impulse);
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
        SetupRigidbody();
    }

    private void SetupRigidbody()
    {
        rb = GetComponent<Rigidbody>();

        rb.useGravity = false;
        rb.mass = mass;
    }

    private void OnDestroy() 
    {
        Universe.OnGravityBodyDestroyed?.Invoke(this);
    }

}
