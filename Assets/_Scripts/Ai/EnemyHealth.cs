using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDestructible
    {
        public int maxHealth = 100;
        private int currentHealth;
         [SerializeField] private AudioSource source;
         [SerializeField] private AudioClip Explode;
    public void TakeDamage(int damageAmount)
    {
     currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
             source.clip = Explode;
             source.Play();
              Destroy(gameObject);
        }
    }
    public void Fracture()
    {

    }
    }
