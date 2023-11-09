using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuBackgroundMusic : MonoBehaviour
{
    private static MenuBackgroundMusic backgroundMusic;

    void Awake()
    {
        if (backgroundMusic == null)
        {
            backgroundMusic = this;
            DontDestroyOnLoad(backgroundMusic);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void StopBackgroundMusic()
    {
        Destroy(gameObject);
    }

   
    public void StopMusicOnSceneChange()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Main-Scene")
        {
            StopBackgroundMusic();
        }
    }
}
