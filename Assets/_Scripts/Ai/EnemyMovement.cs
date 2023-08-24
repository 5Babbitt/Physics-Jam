using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
[SerializeField] Transform target;
[SerializeField] float rotationalDamp = 5f;
[SerializeField] float movementSpeed = 10f;





void Update()
{
turn();
move();

}
void turn()
{
 Vector3 pos = target.position - transform.position;   
 Quaternion rotation = Quaternion.LookRotation(pos);
 transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationalDamp * Time.deltaTime);
}
void move()
{
  transform.position += transform.forward * movementSpeed * Time.deltaTime;
}


}
