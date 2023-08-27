using UnityEngine;

public class ShipWeaponSystem : ShipSystem
{
    [Header("Torpedo Settings")]
    [SerializeField] bool canFireTorpedo = true;
    [SerializeField] int NumTorpedoes;
    [SerializeField] private float weaponCooldown;
    [SerializeField] private float timeTillCanFire;

    [Space(5)]
    [SerializeField] private GameObject torpedo;

    [Header("General Settings")]
    [SerializeField] private Transform[] firePoints;

    protected override void Awake() 
    {
        base.Awake();

        
    }

    private void OnEnable() 
    {
        ship.id.Events.OnFireInput += OnFireInput;
    }

    private void OnDisable() 
    {
        ship.id.Events.OnFireInput -= OnFireInput;
    }

    private void Update() 
    {
        timeTillCanFire -= Time.deltaTime;

        if (timeTillCanFire < 0) 
            canFireTorpedo = true;
    }

    private void OnFireInput()
    {
        if (canFireTorpedo)
            FireTorpedo();
    }

    private void FireTorpedo()
    {
        NumTorpedoes--;

        if (NumTorpedoes <= 0)
            return;
        
        foreach (Transform t in firePoints)
        {
            Instantiate(torpedo, t.position, t.rotation).GetComponent<Torpedo>();
            ship.id.Events.OnTorpedoFired?.Invoke();
        }
        
        ResetCooldown();
    }

    private void ResetCooldown()
    {
        timeTillCanFire = weaponCooldown;

        canFireTorpedo = false;
    }
}
