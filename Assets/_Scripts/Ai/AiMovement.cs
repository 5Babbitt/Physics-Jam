using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiMovement : MonoBehaviour
{
    public Transform player;
    public float chaseSpeed;
    public float rotationSpeed;
    public float retreatDuration;
    public float retreatCooldown;
    public float minimumDistance;

    private Vector3 initialPosition;
    private bool isRetreating = false;
    private bool isCoolingDown = false;
    private float retreatTimer = 0f;
    private float cooldownTimer = 0f;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > minimumDistance)
        {
            if (!isRetreating && !isCoolingDown)
            {
                ChasePlayer();
            }
            else if (isRetreating)
            {
                Retreat();
            }
            else if (isCoolingDown)
            {
                Cooldown();
            }
        }
        else
        {
            if (!isRetreating && !isCoolingDown)
            {
                StartRetreat();
            }
        }
    }

    private void ChasePlayer()
    {
        Vector3 targetDirection = player.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        transform.Translate(Vector3.forward * chaseSpeed * Time.deltaTime);
    }

    private void StartRetreat()
    {
        isRetreating = true;
        retreatTimer = 0f;
    }

    private void Retreat()
    {
        retreatTimer += Time.deltaTime;

        if (retreatTimer >= retreatDuration)
        {
            isRetreating = false;
            isCoolingDown = true;
        }
    }

    private void Cooldown()
    {
        cooldownTimer += Time.deltaTime;

        if (cooldownTimer >= retreatCooldown)
        {
            isCoolingDown = false;
            cooldownTimer = 0f;
        }
    }
}