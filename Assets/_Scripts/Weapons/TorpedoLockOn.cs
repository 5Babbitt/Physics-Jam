using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorpedoLockOn : MonoBehaviour
{
    private Transform target;
    private float lockDuration;
    private float lockTimer;
    public float speed;
    public float rotationSpeed;
    public void LockOntoTarget(Transform newTarget)
    {
        target = newTarget;
        lockTimer = lockDuration;
    }
    private void Update()
    {
        if (target != null)
        {
            Vector3 targetPosition = target.position;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed); 

            Vector3 direction = targetPosition - transform.position;

            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed); 
            lockTimer -= Time.deltaTime;
            if (lockTimer <= 0.0f)
            {
                target = null; 
            }
        }
    }
}