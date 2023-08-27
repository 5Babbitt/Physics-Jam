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
    private bool applyThrustLastFrame;
    
    [Header("Rigidbody Values")]
    [SerializeField] private float mass;
    [SerializeField] private float angularDrag;

    [Header("Speed Values")]
    [SerializeField] private float thrustSpeed;
    [SerializeField] private float angularSpeed;
    [SerializeField] private AnimationCurve thrustFromDot;

    [Header("Movement Values")]
    [SerializeField] private bool hasFuel = true;
    private Vector3 pitch;
    private Vector3 yaw;
    private Vector3 roll;

    protected override void Awake() 
    {
        base.Awake();

        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        ship.id.Events.OnTurnInput += OnTurnInput;
        ship.id.Events.OnThrustInput += OnThrustInput;
        ship.id.Events.OnFuelEmpty += OnFuelEmpty;
        ship.id.Events.OnFuelRefilled += OnFuelRefilled;
    }

    private void OnDisable() 
    {
        ship.id.Events.OnTurnInput -= OnTurnInput;
        ship.id.Events.OnThrustInput -= OnThrustInput;
        ship.id.Events.OnFuelEmpty -= OnFuelEmpty;
        ship.id.Events.OnFuelRefilled += OnFuelRefilled;
    }

    private void Update() 
    {
        CalculateTorque();

        if (applyThrust != applyThrustLastFrame)
        {
            ship.id.Events.OnThrustValueChanged?.Invoke(applyThrust);
        }
    }

    private void LateUpdate() 
    {
        applyThrustLastFrame = applyThrust;
    }

    private void FixedUpdate()
    {
        if (applyThrust && hasFuel)
            ApplyThrust();

        ApplyTorque();
    }

    private void ApplyThrust()
    {
        int thrustInput = applyThrust ? 1 : 0;

        float directionMultiplier = thrustFromDot.Evaluate(Vector3.Dot(transform.forward, rb.velocity.normalized));
        
        rb.AddForce(thrustInput * mass * thrustSpeed * directionMultiplier * transform.forward);

        ship.id.Events.OnThrust?.Invoke();
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

    private void OnFuelEmpty()
    {
        hasFuel = false;
    }

    private void OnFuelRefilled()
    {
        hasFuel = true;
    }

    private void OnValidate() 
    {
        rb = GetComponent<Rigidbody>();
        
        mass = rb.mass;
        rb.angularDrag = angularDrag;
    }
}
