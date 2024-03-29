using System;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[CreateAssetMenu]
public class ShipID : ScriptableObject
{
    public string Name;
    public Color ShipColor;

    public ShipEvents Events;

    public Vector3 HyperspaceLocation;
    public Vector3 spawnPosition;
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
    public Action<bool> OnThrustValueChanged;
    public Action<float> OnFuelChanged;
    public Action OnFuelEmpty;
    public Action OnFuelRefilled;

    // Teleport Events
    public Action OnHyperdriveActivated;
    public Action OnEnteredHyperspace;
    public Action OnExitHyperspace;

    // Combat Events
    public Action<int> OnTakeDamage;
    public Action OnShipExplode;
    public Action OnTorpedoFired;

    // UI Events
    public Action<int> OnHealthChanged;
    public Action<int> OnTorpedoCountChanged;
    public Action<float> OnHyperspaceCooldownChanged;
}
