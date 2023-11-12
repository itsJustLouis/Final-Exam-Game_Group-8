using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Main-Scene");
    }
    public void IntroToGame()
    {
        SceneManager.LoadScene("IntroCutScene");
    }
    public void StartTut()
    {
        
        SceneManager.LoadScene("Tutorial");
    }
}
