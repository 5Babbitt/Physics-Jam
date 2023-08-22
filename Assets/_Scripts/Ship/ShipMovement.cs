using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : ShipSystem
{
    public Rigidbody rb;
    
    // Input
    private Vector2 rotationinput;
    private bool applyThrust;
    
    [Header("Rigidbody Values")]
    [SerializeField] private float mass;
    [SerializeField] private float angularDrag;

    [Header("Speed Values")]
    [SerializeField] private float thrustSpeed;
    [SerializeField] private float angularSpeed;

    protected override void Awake() 
    {
        base.Awake();

        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        ship.id.Events.OnTurnInput += OnTurnInput;
        ship.id.Events.OnThrustInput += OnThrustInput;
    }

    private void OnDisable() 
    {
        ship.id.Events.OnTurnInput -= OnTurnInput;
        ship.id.Events.OnThrustInput -= OnThrustInput;
    }

    private void Update() 
    {
        
    }

    private void FixedUpdate()
    {
        if (applyThrust)
            ApplyThrust();

        ApplyTorque();
    }

    private void ApplyThrust()
    {
        int thrustInput = applyThrust ? 1 : 0;
        
        rb.AddForce(thrustInput * mass * thrustSpeed * transform.forward);

        ship.id.Events.OnThrustUsed?.Invoke();
    }

    private void ApplyTorque()
    {
        Vector3 pitch = transform.right * rotationinput.y;
        Vector3 yaw = transform.up * rotationinput.x;
        
        Vector3 _rotation = pitch + yaw;
        
        rb.AddTorque(_rotation * mass * angularSpeed * Mathf.Deg2Rad);
    }

    private void OnThrustInput(bool value)
    {
        applyThrust = value;
    }

    private void OnTurnInput(Vector2 vector)
    {
        rotationinput = vector;
    }

    private void OnValidate() 
    {
        rb = GetComponent<Rigidbody>();
        
        rb.mass = mass;
        rb.angularDrag = angularDrag;
    }
}
