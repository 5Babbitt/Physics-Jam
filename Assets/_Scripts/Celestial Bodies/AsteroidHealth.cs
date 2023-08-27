using UnityEngine;

public class AsteroidHealth : MonoBehaviour, IDestructible
{
    public int health = 1;

    private void OnCollisionEnter(Collision other) 
    {
        var body = other.gameObject.GetComponent<GravityBody>();

        if (body != null)
        {
            TakeDamage(100);
        }
    }

    public void Fracture()
    {
        Debug.Log("Asteroid Destroyed");
    }

    public void TakeDamage(int amount)
    {
        health -= amount;

        if (health < 0)
        {
            Fracture();
            Destroy(gameObject);
        }
    }

    private void OnDestroy() 
    {
        
    }
}
