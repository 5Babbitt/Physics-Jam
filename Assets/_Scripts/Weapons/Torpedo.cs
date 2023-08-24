using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class Torpedo : MonoBehaviour
{
    public Rigidbody rb { get; private set; }
    [field:SerializeField] public float velocity { get; private set; }

    [SerializeField] private float lifetime;
    [SerializeField] private float currentLifetime;

    private void Awake() 
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    private void Start() 
    {
        rb.velocity = transform.forward * velocity;

        currentLifetime = lifetime;
    }

    private void Update() 
    {
        currentLifetime -= Time.deltaTime;

        if (currentLifetime < 0)
        {
            Destroy(gameObject);
        }
    }
}
