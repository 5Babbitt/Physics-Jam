using UnityEngine;
using UnityEngine.VFX;

public class AsteroidHealth : MonoBehaviour, IDestructible
{
    public int health = 20;
    public GravityBody body;

    public GameObject explosion;

    private void Awake() 
    {
        body = GetComponent<GravityBody>();
    }
    

    private void OnCollisionEnter(Collision other) 
    {
        var body = other.gameObject.GetComponent<GravityBody>();

        if (body.bodyType == BodyTypes.asteroid)
        {
            return;
        }

        if(body.bodyType == BodyTypes.star)
            Destroy(gameObject);

        TakeDamage(100);
    }

    public void Fracture()
    {
        Debug.Log("Asteroid Destroyed");

        SpawnFracturedPieces();

        Instantiate(explosion, transform.position, Quaternion.identity);
    }

    private void SpawnFracturedPieces()
    {
        int numPieces = Random.Range(3, 7);

        for (int i = 0; i < numPieces; i++)
        {
            float explosiveForce = Random.Range(1f, 10f);

            UniverseSimManager.Instance.SpawnAsteroid(transform.position, Random.rotation, explosiveForce, body.radius / numPieces, body.surfaceGravity / numPieces);
        }
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

    private void OnDisable() 
    {
        UniverseSimManager.Instance.OnGravityBodyDestroyed(GetComponent<GravityBody>());
    }
}
