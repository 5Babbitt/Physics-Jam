using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Animations.Rigging;

[RequireComponent(typeof(SphereCollider), typeof(Rigidbody))]
public class FieldManager : Singleton<FieldManager>
{
    private Rigidbody rb;
    private SphereCollider col;

    [SerializeField] private Vector3 centre = Vector3.zero;
    [SerializeField] private float radius = 5000f;
    
    public float Radius { get { return radius; } }
    public Vector3 Centre { get { return centre; } }

    protected override void Awake() 
    {
        base.Awake();

        SetComponents();
    }

    private void SetComponents()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<SphereCollider>();

        rb.isKinematic = true;
        rb.useGravity = false;

        col.isTrigger = true;
        col.center = centre;
        col.radius = radius;
    }

    private void OnTriggerExit(Collider other) 
    {
        var body = other.transform.root.gameObject;

        Debug.Log("object exited");

        var ship = body.GetComponent<Ship>();

        if (ship != null)
        {    
            Debug.Log("ship exited");

            Rigidbody shipRB = body.GetComponent<Rigidbody>();
            Debug.Log(shipRB.velocity);
            
            // other.GetComponent<ShipHyperdrive>();
            Teleport(shipRB.gameObject, Vector3.one * 10000);
            
            shipRB.velocity = Vector3.zero;
            TeleportShip(body, CalculateTeleportPointOnSphere(body.transform.position, centre), shipRB.velocity);
            return;
        }
        
        Teleport(body, CalculateTeleportPointOnSphere(body.transform.position, centre));
    }

    private Vector3 CalculateTeleportPointOnSphere(Vector3 position, Vector3 centre)
    {
        Vector3 heading = position - centre;

        return centre - heading;
    }

    private IEnumerator TeleportShip(GameObject body, Vector3 position, Vector3 velocity)
    {
        Debug.Log("Ship Teleport Commencing");
        
        yield return new WaitForSeconds(3f);

        Debug.Log("Ship Teleported");
        Teleport(body, position, velocity);
    }

    private void Teleport(GameObject body, Vector3 position)
    {
        var trail = body.GetComponentInChildren<TrailRenderer>();

        if (trail != null)
            trail.emitting = false;

        body.transform.position = position;

        if (trail != null)
            trail.emitting = true;
    }   

    private void Teleport(GameObject body, Vector3 position, Vector3 velocity)
    {
        body.transform.position = position;
        body.GetComponent<Rigidbody>().velocity = velocity;
    } 

    private void OnDrawGizmos() 
    {
        Gizmos.DrawWireSphere(centre, col.radius);
    }

    private void OnValidate() 
    {
        SetComponents();
    }
}
