using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : ShipSystem
{
    public Rigidbody rb;
    
    // Input
    private float pitchInput;
    private float yawInput;
    private float rollInput;
    private bool applyThrust;
    
    [Header("Rigidbody Values")]
    [SerializeField] private float mass;
    [SerializeField] private float angularDrag;

    [Header("Speed Values")]
    [SerializeField] private float thrustSpeed;
    [SerializeField] private float angularSpeed;

    [Header("Movement Values")]
    [SerializeField] private Vector3 pitch;
    [SerializeField] private Vector3 yaw;
    [SerializeField] private Vector3 roll;

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
        CalculateTorque();
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

    private void CalculateTorque()
    {
        pitch = transform.right * pitchInput;
        yaw = transform.up * yawInput;
        roll = transform.forward * rollInput;
    }

    private void ApplyTorque()
    {
        Vector3 _rotation = pitch + yaw + roll;
        
        rb.AddTorque(_rotation * mass * angularSpeed * Mathf.Deg2Rad);
    }

    private void OnThrustInput(bool value)
    {
        applyThrust = value;
    }

    private void OnTurnInput(Vector3 vector)
    {
        pitchInput = -vector.y;
        yawInput = vector.x;
        rollInput = -vector.z;
    }

    private void OnValidate() 
    {
        rb = GetComponent<Rigidbody>();
        
        rb.mass = mass;
        rb.angularDrag = angularDrag;
    }
}