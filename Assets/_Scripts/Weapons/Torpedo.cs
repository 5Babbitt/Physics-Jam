using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class Torpedo : MonoBehaviour
{
    public Rigidbody rb { get; private set; }
    [field:SerializeField] public float velocity { get; private set; }

    private void Awake() 
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;

    }

    private void Start() 
    {
        PhysicsHelper.ApplyForceToReachVelocity(rb, velocity * transform.forward, rb.mass * velocity, ForceMode.Impulse);
    }
}
