using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject GameMusic;
    public bool isPaused;
    void Start()
    {
     pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
        if(isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
        }
    }

    void PauseGame()
    {
         pauseMenu.SetActive(true);
         Time.timeScale = 0f;
         isPaused = true;
         GameMusic.SetActive(false);
    }
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
         Time.timeScale = 0f;
         isPaused = false;
         GameMusic.SetActive(true);
    }
    public void MainMenu()
    {
    SceneManager.LoadSceneAsync(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
