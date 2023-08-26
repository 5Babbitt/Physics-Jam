using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform playerSpaceship;
    public float moveSpeed = 10.0f;
    public float rollSpeed = 5.0f;
    public float dodgeForce = 20.0f;
    public float dodgeCooldown = 2.0f;
    public float desiredDistance = 15.0f;
    public float shootingCooldown = 1.0f;
    public int maxHealth = 100;
    public float shootingVelocity;

    public Transform[] firePoints;
    public GameObject bulletPrefab;

    private int currentHealth;
    private float lastDodgeTime;
    private float lastShootTime;

    private void Start()
    {
        currentHealth = maxHealth;
        lastDodgeTime = -dodgeCooldown;
        lastShootTime = -shootingCooldown;
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject); 
            return;
        }

        Vector3 directionToPlayer = playerSpaceship.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

       
        if (Time.time - lastDodgeTime > dodgeCooldown && distanceToPlayer < desiredDistance * 0.5f)
        {
            DodgeRoll(directionToPlayer);
        }

        if (distanceToPlayer > desiredDistance)
        {
            Vector3 avoidDirection = -directionToPlayer.normalized;
            transform.Translate(avoidDirection * moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(transform.forward * moveSpeed * Time.deltaTime);
        }
        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);
        if (Time.time - lastShootTime > shootingCooldown && distanceToPlayer < desiredDistance)
        {
            Shoot();
        }
    }

    private void DodgeRoll(Vector3 dodgeDirection)
    {
        lastDodgeTime = Time.time;

        Vector3 dodgeRollDirection = Vector3.Cross(dodgeDirection, Vector3.up);
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddTorque(dodgeRollDirection * rollSpeed, ForceMode.Impulse);
        rb.AddForce(dodgeDirection.normalized * dodgeForce, ForceMode.Impulse);
    }

    private void Shoot()
    {
        lastShootTime = Time.time;

        foreach (Transform firePoint in firePoints)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
          
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            bulletRb.velocity = firePoint.forward * shootingVelocity; 
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
    }
}