using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDestructible
    {
        public int maxHealth = 100;
        private int currentHealth;
    public void TakeDamage(int damageAmount)
    {
     currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
              Destroy(gameObject);
        }
    }
    public void Fracture()
    {

    }
    }
