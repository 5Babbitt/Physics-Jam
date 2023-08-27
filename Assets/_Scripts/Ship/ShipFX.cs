using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ShipFX : ShipSystem
{
    [SerializeField] private AudioClip thrust;
    [SerializeField] private AudioClip shoot;
    private AudioSource thrustSource;
    private AudioSource shootSource;

    [SerializeField] private GameObject explosion;

    protected override void Awake()
    {
        base.Awake();

        thrustSource = gameObject.GetComponent<AudioSource>();
        shootSource = gameObject.AddComponent<AudioSource>();

        thrustSource.clip = thrust;
        shootSource.volume = 0.05f;
    }

    private void OnEnable() 
    {
        ship.id.Events.OnTorpedoFired += OnTorpedoFired;
        ship.id.Events.OnThrustValueChanged += OnThrust;
        ship.id.Events.OnShipExplode += OnShipExplode;
        ship.id.Events.OnTakeDamage += OnTakeDamage;
        ship.id.Events.OnEnteredHyperspace += OnEnteredHyperspace;
        ship.id.Events.OnExitHyperspace += OnExitHyperspace;
    }

    private void OnDisable() 
    {
        ship.id.Events.OnTorpedoFired -= OnTorpedoFired;
        ship.id.Events.OnThrustValueChanged += OnThrust;
        ship.id.Events.OnShipExplode -= OnShipExplode;
        ship.id.Events.OnTakeDamage -= OnTakeDamage;
        ship.id.Events.OnEnteredHyperspace -= OnEnteredHyperspace;
        ship.id.Events.OnExitHyperspace -= OnExitHyperspace;
    }

    private void Update() 
    {
        
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
        source.PlayOneShot(clip);
    }

    private void PlaySource(AudioSource source)
    {
        source.Play();
    }

    private void PauseSource(AudioSource source)
    {
        source.Pause();
    }

    private void OnShipExplode()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
    }

    private void OnThrust(bool value)
    {
        if (value)
            PlaySource(thrustSource);
        else if (!value)
            PauseSource(thrustSource);
    }


    private void OnTorpedoFired()
    {
        PlaySound(shootSource, shoot);
    }
}