using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    public GameObject player; 
    public GameObject torpedoPrefab; 
    public Transform[] firePoints; 
    public float fireCooldown; 
    public float torpedoSpeed; 
    public float detectionRange;

    private bool canFire = true;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= detectionRange)
        {
            Vector3 targetDirection = player.transform.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime);
            if (canFire)
            {
                StartCoroutine(FireTorpedoes());
            }
        }
    }

    private IEnumerator FireTorpedoes()
    {
        canFire = false;

        foreach (Transform firePoint in firePoints)
        {
            GameObject torpedo = Instantiate(torpedoPrefab, firePoint.position, firePoint.rotation);
            Rigidbody torpedoRigidbody = torpedo.GetComponent<Rigidbody>();
            torpedoRigidbody.velocity = torpedo.transform.forward * torpedoSpeed;
            Destroy(torpedo, 10.0f);
        }

        yield return new WaitForSeconds(fireCooldown);
        canFire = true;
    }
}