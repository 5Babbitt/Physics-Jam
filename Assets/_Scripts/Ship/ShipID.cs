using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ShipID : ScriptableObject
{
    public string Name;
    public Color ShipColor;

    public ShipEvents Events;

}

public struct ShipEvents
{
    // Input Events
    public Action<Vector3> OnTurnInput;
    public Action<bool> OnThrustInput;
    public Action OnFireInput;
    public Action OnHyperdriveInput;

    // Movement Events
    public Action OnThrust;
    public Action OnHyperdriveActivated;
    public Action<float> OnFuelChanged;
    public Action OnFuelEmpty;
    public Action OnFuelRefilled;

    // Collision Events
    public Action<int> OnTakeDamage;

    // UI Events


}
