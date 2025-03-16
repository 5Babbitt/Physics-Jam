using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    public GameObject player; 
    public GameObject torpedoPrefab; 
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip shoot;
    public Transform[] firePoints; 
    public float fireCooldown; 
    public float torpedoSpeed; 
    public float detectionRange;

    private bool canFire = true;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Ship");
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
            torpedoRigidbody.linearVelocity = torpedo.transform.forward * torpedoSpeed;
            Destroy(torpedo, 10.0f);
            OnTorpedoFired();
   
        }

        yield return new WaitForSeconds(fireCooldown);
        canFire = true;
    }
      private void OnTorpedoFired()
    {
        source.clip = shoot;
        source.Play();
    }
}