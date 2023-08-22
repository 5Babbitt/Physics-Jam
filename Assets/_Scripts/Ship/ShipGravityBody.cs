using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipGravityBody : MonoBehaviour
{
    public float mass;

    private Vector3 currentForce;

    public Rigidbody rb;
    public Rigidbody Rigidbody { get { return rb; } }
    
}
