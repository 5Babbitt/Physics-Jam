using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class ShipInput : ShipSystem
{
    private PlayerInput input;
    
    [SerializeField] private Vector3 rotationInput;
    [SerializeField] private bool thrustInput;
    [SerializeField] private bool fireInput;
    [SerializeField] private bool hyperdriveInput;
    
    protected override void Awake() 
    {
        base.Awake();

        input = GetComponent<PlayerInput>();
    }
    
    private void Update() 
    {
        GetInput();

        ship.id.Events.OnTurnInput?.Invoke(rotationInput);
        ship.id.Events.OnThrustInput?.Invoke(thrustInput);
        
        if (hyperdriveInput)
            ship.id.Events.OnHyperdriveInput?.Invoke();

        if (fireInput)
            ship.id.Events.OnFireInput?.Invoke();
    }

    private void GetInput()
    {
        rotationInput = input.actions["Rotation"].ReadValue<Vector3>();
        thrustInput = input.actions["Thrust"].IsPressed();
        fireInput = input.actions["Fire"].WasPressedThisFrame();
        hyperdriveInput = input.actions["Hyperspace"].WasPressedThisFrame();
    }
}
