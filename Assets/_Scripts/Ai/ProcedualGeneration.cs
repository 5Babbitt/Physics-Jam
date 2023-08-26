using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcedualGeneration : MonoBehaviour
{
    public GameObject[] itemPrefabs;
    public int numberOfItems;
    public float spawnRadius;  
    private void Start()
    {
        SpawnRandomItems();
    }

    private void SpawnRandomItems()
    {
        for (int i = 0; i < numberOfItems; i++)
        {
            GameObject selectedPrefab = itemPrefabs[Random.Range(0, itemPrefabs.Length)];

            Vector3 randomPosition = transform.position + Random.insideUnitSphere * spawnRadius;

            Quaternion randomRotation = Quaternion.Euler(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));

            Vector3 randomScale = Vector3.one * Random.Range(0.5f, 2f);
            GameObject spawnedItem = Instantiate(selectedPrefab, randomPosition, randomRotation);
            spawnedItem.transform.localScale = randomScale;
        }
    }
}