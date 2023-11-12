using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public GameObject PausePanel;

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0) // Change 0 to the index of your scene
        {
            // Reset your pause state when the scene is reloaded
            PausePanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }
    public void Pause()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Continue()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void StartScreen()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("START SCREEN");
    }
    public void ReloadGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main-Scene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
