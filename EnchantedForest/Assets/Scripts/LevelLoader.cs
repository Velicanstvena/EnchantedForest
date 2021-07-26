using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    int currentSceneIndex;

    // Start is called before the first frame update
    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Start Screen");
    }

    public void LoadOptionsScreen()
    {
        SceneManager.LoadScene("Options Screen");
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadYouLose()
    {
        SceneManager.LoadScene("Lose Screen");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
