using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipFuel : ShipSystem
{
    [SerializeField] private float fuelCapacity;
    [SerializeField] private float currentFuelLevel;
    [SerializeField] private float flowrate;

    protected override void Awake() 
    {
        base.Awake();
    }

    private void OnEnable() 
    {
        ship.id.Events.OnThrust += ReduceFuel;
    }

    private void OnDisable() 
    {
        ship.id.Events.OnThrust -= ReduceFuel;
    }

    void ReduceFuel()
    {
        currentFuelLevel -= flowrate * Time.deltaTime;

        if (currentFuelLevel < 0)
        {
            ship.id.Events.OnFuelEmpty?.Invoke();
        }

        ship.id.Events.OnFuelChanged?.Invoke(currentFuelLevel);
    }
}
