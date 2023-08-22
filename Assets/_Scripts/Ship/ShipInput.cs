using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class ShipInput : ShipSystem
{
    private PlayerInput input;
    
    [SerializeField] private Vector2 rotationInput;
    [SerializeField] private bool thrustInput;
    [SerializeField] private bool fireInput;
    
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

        if (fireInput)
            ship.id.Events.OnFireInput?.Invoke();
    }

    private void GetInput()
    {
        rotationInput = input.actions["Rotation"].ReadValue<Vector2>();
        thrustInput = input.actions["Thrust"].IsPressed();
        fireInput = input.actions["Fire"].WasPressedThisFrame();
    }
}
