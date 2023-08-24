using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class AiController : MonoBehaviour
{
   public NavMeshAgent agent;
   public Transform player;
   public LayerMask whatIsPlayer;
   public GameObject projectile;
   public float health;

  // Attacking
  public float timeBetweenAttacks;
  bool alreadyAttacked;

  // States
  public float sightRange, attackRange;
  public bool playerInRange, playerInAttackRange;

  private void Awake()
  {
    player = GameObject.Find("Ship").transform;
    agent = GetComponent<NavMeshAgent>();
  }

  void Update()
  {
    //check for sight and attack range
    playerInRange = Physics.CheckSphere(transform.position,sightRange,whatIsPlayer);
    playerInAttackRange = Physics.CheckSphere(transform.position,attackRange,whatIsPlayer);

    if (playerInRange && !playerInAttackRange) chasePlayer();
    if (playerInAttackRange && playerInRange) attackPlayer();
  }
  private void attackPlayer()
  {
    agent.SetDestination(transform.position);
  }

   private void chasePlayer()
  {
    agent.SetDestination(player.position);
    transform.LookAt(player);

    if (!alreadyAttacked)
    {
      Rigidbody rb = Instantiate(projectile,transform.position, Quaternion.identity).GetComponent<Rigidbody>();
      rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
       rb.AddForce(transform.up * 32f, ForceMode.Impulse);
      
      alreadyAttacked = true;
      Invoke(nameof(resetAttack), timeBetweenAttacks);
    }
  }
  private void resetAttack()
  {
   alreadyAttacked = false;
  }

  public void takeDamage(int damage)
  {
  health -= damage; 
  if (health < 0) Invoke(nameof(destroyEnemy),0.5f);
  }

  public void destroyEnemy()
  {
    Destroy(gameObject);
  }
  private void onGizmosSelected()
  {
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(transform.position, attackRange);
     Gizmos.color = Color.green;
    Gizmos.DrawWireSphere(transform.position, sightRange);
  }
   
}
