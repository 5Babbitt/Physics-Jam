using UnityEngine;

public class ShipHealth : ShipSystem, IDestructible
{
    public int maxHealth = 50;
    public int currentHealth;

    protected override void Awake() 
    {
        base.Awake();
    }

    private void Start() 
    {
        currentHealth = maxHealth;
    }

    private void OnEnable() 
    {
        ship.id.Events.OnTakeDamage += TakeDamage;
    }

    private void OnDisable() 
    {
        ship.id.Events.OnTakeDamage -= TakeDamage;
    }

    private void OnCollisionEnter(Collision other) 
    {
        var body = other.gameObject.GetComponent<GravityBody>();

        if (body != null)
        {
            if (body.bodyType == BodyTypes.star || body.bodyType == BodyTypes.planet || body.bodyType == BodyTypes.ship)
                TakeDamage(maxHealth);
            else
                TakeDamage(10);
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Fracture();

            ship.id.Events.OnShipExplode?.Invoke();

            Destroy(gameObject);
        }
    }
    
    public void Fracture()
    {
        Debug.Log("Ship Destroyed");
    }

    private void OnDestroy() 
    {
        UniverseSimManager.Instance.OnGravityBodyDestroyed(GetComponent<GravityBody>());
    }
}
