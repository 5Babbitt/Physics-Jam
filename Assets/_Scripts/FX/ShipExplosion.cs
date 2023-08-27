using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(AudioSource))]
public class ShipExplosion : MonoBehaviour
{
    [SerializeField] private AudioClip explosion;
    [SerializeField] private VisualEffect explosionEffect;
    
    private AudioSource explosionSource;

    private void Awake() 
    {
        explosionSource = gameObject.AddComponent<AudioSource>();
        
        explosionSource.PlayOneShot(explosion);
    }
}
