using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string sceneOnTag1Destroyed = "SceneTag1Destroyed"; 
    public string sceneOnTag2Destroyed = "SceneTag2Destroyed"; 

    private void Update()
    {
       
        GameObject[] tag1Objects = GameObject.FindGameObjectsWithTag("Ship");
        GameObject[] Enemy = GameObject.FindGameObjectsWithTag("Enemy");

       
        foreach (GameObject obj in tag1Objects)
        {
            if (obj == null)
            {
                LoadSceneOnTag1Destroyed();
                break; 
            }
        }

        foreach (GameObject obj in Enemy)
        {
            if (obj == null)
            {
                LoadSceneOnTag2Destroyed();
                break; 
            }
        }
    }

    private void LoadSceneOnTag1Destroyed()
    {
        SceneManager.LoadSceneAsync(4);
    }

    private void LoadSceneOnTag2Destroyed()
    {
        SceneManager.LoadSceneAsync(3);
    }
}
