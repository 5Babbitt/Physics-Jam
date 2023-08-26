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
    [SerializeField] private float radius = 2500f;
    
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

        if (ship != null && ship.GetComponent<ShipHyperdrive>().isWarping == false)
        {    
            Debug.Log("ship exited");
            
            // other.GetComponent<ShipHyperdrive>();

            StartCoroutine(TeleportShip(body, CalculateTeleportPointOnSphere(body.transform.position, centre)));
            return;
        }
        
        Teleport(body, CalculateTeleportPointOnSphere(body.transform.position, centre));
    }

    private Vector3 CalculateTeleportPointOnSphere(Vector3 position, Vector3 centre)
    {
        Vector3 heading = position - centre;

        return centre - heading;
    }

    private IEnumerator TeleportShip(GameObject body, Vector3 position)
    {
        Rigidbody shipRB = body.GetComponent<Rigidbody>();
        Vector3 velocity = shipRB.velocity;
        
        Debug.Log("Ship Teleport Commencing");
        Teleport(body, Vector3.one * 10000);
            
        shipRB.velocity = Vector3.zero;
        
        yield return new WaitForSeconds(3f);

        Debug.Log("Ship Teleported");
        Teleport(body, position, velocity);
    }

    private void Teleport(GameObject body, Vector3 position)
    {
        body.transform.position = position;
    }   

    private void Teleport(GameObject body, Vector3 position, Vector3 velocity)
    {
        body.transform.position = position;
        body.GetComponent<Rigidbody>().velocity = velocity / 2;
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
