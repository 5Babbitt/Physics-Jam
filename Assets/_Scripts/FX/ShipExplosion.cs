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

    private float lifetime = 2f;
    private float currentLife;

    private void Awake() 
    {
        explosionSource = gameObject.GetComponent<AudioSource>();
        
        explosionSource.PlayOneShot(explosion);

        explosionEffect.Play();
    }

    private void Start() 
    {
        currentLife = lifetime;
    }

    private void Update() 
    {
        currentLife -= Time.deltaTime;

        if (currentLife < 0)
            Destroy(gameObject);
    }
}
