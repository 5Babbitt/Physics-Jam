using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void multiplayer()
    {
        SceneManager.LoadSceneAsync(2);
    }

    public void OpenSettings()
    {
        SceneManager.LoadSceneAsync(5);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}