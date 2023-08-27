using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ShipFX : ShipSystem
{
    [SerializeField] private AudioClip explosion;
    [SerializeField] private AudioClip thrust;
    [SerializeField] private AudioClip shoot;
    private AudioSource explosionSource;
    private AudioSource thrustSource;
    private AudioSource shootSource;

    protected override void Awake()
    {
        base.Awake();

        explosionSource = gameObject.AddComponent<AudioSource>();
        thrustSource = gameObject.AddComponent<AudioSource>();
        shootSource = gameObject.AddComponent<AudioSource>();
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
        
    }

    private void OnEnteredHyperspace()
    {
      
    }

    private void OnTakeDamage(int value)
    {
      
    }

    private void PlaySound(AudioSource source, AudioClip clip)
    {
        source.clip = clip;
        source.Play();
    }

    private void OnShipExplode()
    {
        PlaySound(explosionSource, explosion);
    }

    private void OnThrust()
    {
        PlaySound(thrustSource, thrust);
    }

    private void OnTorpedoFired()
    {
        PlaySound(shootSource, shoot);
    }
}