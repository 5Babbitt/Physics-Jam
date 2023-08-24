using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipFuel : ShipSystem
{
    [SerializeField] private float fuelCapacity;
    [SerializeField] private float currentFuelLevel;
    [SerializeField] private float burnRate;

    protected override void Awake() 
    {
        base.Awake();
    }

    private void OnEnable() 
    {
        
    }

    private void OnDisable() 
    {
        
    }
}
