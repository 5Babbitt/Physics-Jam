using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
[SerializeField] Transform target;

void Update()
{
    InFront();
}

bool InFront()
{
Vector3 directionToTarget = transform.position - transform.position;
float angle = Vector3.Angle(transform.forward, directionToTarget);

//in range
if (MathF.Abs(angle) < 90 && MathF.Abs(angle) < 270)
{
    Debug.DrawLine(transform.position, target.position, Color.green);
    return true;
}
    Debug.DrawLine(transform.position, target.position, Color.green);
    return false;

}

}

