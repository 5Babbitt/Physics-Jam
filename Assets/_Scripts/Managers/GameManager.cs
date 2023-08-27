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
     if (GameObject.FindGameObjectsWithTag("Enemy") == null)
     {
      EnemyDies();
     }
     if (GameObject.FindGameObjectsWithTag("Ship") == null)
     {
        ShipDies();
     }
    }

    void ShipDies()
    {
    SceneManager.LoadSceneAsync(4);
    }
    void EnemyDies()
    {
    SceneManager.LoadSceneAsync(3);
    }
}