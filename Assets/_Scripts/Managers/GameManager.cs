using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject Ship;
    public GameObject Enemy;

    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] ships = GameObject.FindGameObjectsWithTag("Ship");

        if (enemies.Length == 0)
        {
            EnemyDies();
        }

        if (ships.Length == 0)
        {
            ShipDies();
        }
    }

    void ShipDies()
    {
        SceneManager.LoadScene(4);
    }

    void EnemyDies()
    {
        SceneManager.LoadScene(3);
    }
}