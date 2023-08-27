using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ShipFX : ShipSystem
{
    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] private AudioSource source;

    protected override void Awake()
    {
        base.Awake();

        source = GetComponent<AudioSource>();
    }

    private void OnEnable() 
    {
        ship.id.Events.OnTorpedoFired += OnTorpedoFired;
        ship.id.Events.OnThrust += OnThrust;
        ship.id.Events.OnShipExplode += OnShipExplode;
        ship.id.Events.OnTakeDamage += OnTakeDamage;
        ship.id.Events.OnEnteredHyperspace += OnEnteredHyperspace;
        ship.id.Events.OnExitHyperspace += OnExitHyperspace;
    }

    private void OnDisable() 
    {
        ship.id.Events.OnTorpedoFired -= OnTorpedoFired;
        ship.id.Events.OnThrust -= OnThrust;
        ship.id.Events.OnShipExplode -= OnShipExplode;
        ship.id.Events.OnTakeDamage -= OnTakeDamage;
        ship.id.Events.OnEnteredHyperspace -= OnEnteredHyperspace;
        ship.id.Events.OnExitHyperspace -= OnExitHyperspace;
    }

    private void OnExitHyperspace()
    {
        throw new NotImplementedException();
    }

    private void OnEnteredHyperspace()
    {
        throw new NotImplementedException();
    }

    private void OnTakeDamage(int value)
    {
        
    }

    private void OnShipExplode()
    {
        
    }

    private void OnThrust()
    {
        
    }

    private void OnTorpedoFired()
    {
        
    }
}
