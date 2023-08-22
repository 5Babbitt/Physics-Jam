using UnityEngine;

public class ShipWeaponSystem : ShipSystem
{
    [SerializeField] private GameObject torpedo;
    
    public int NumTorpedoes;

    [SerializeField] private float weaponCooldown;
    [SerializeField] private Transform firePoint;

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

    private void OnFireInput()
    {
        FireTorpedo();
    }

    private void FireTorpedo()
    {
        Instantiate(torpedo, firePoint.position, firePoint.rotation).GetComponent<Torpedo>();
    }
}
