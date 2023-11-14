using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class KeyCollection : MonoBehaviour
{
    public int keysToCollect = 4;
    public Text keysText;
    public GameObject GameWonSlide;
    private int collectedKeys = 0;
    public AudioClip gameOverSound;
    private bool gameOverSoundPlayed = false;

    void Start()
    {
        GameWonSlide.SetActive(false);
        UpdateKeysText();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Key") || other.CompareTag("Key1") || other.CompareTag("Key2") || other.CompareTag("Key3"))
        {
            CollectKey(other.gameObject);
        }
    }

    void CollectKey(GameObject key)
    {
        key.SetActive(false);  // Disable the collected key
        collectedKeys++;

        UpdateKeysText();

        if (collectedKeys == keysToCollect)
        {
            GameOver();
        }
    }

    void UpdateKeysText()
    {
        if (keysText != null)
        {
            keysText.text = "Keys Left: " + (keysToCollect - collectedKeys);
        }
    }

    void GameOver()
    {
     //   Debug.Log("Game Complete - All keys collected!");
        GameWonSlide.SetActive(true);
        if (!gameOverSoundPlayed)
        {
            if (gameOverSound != null)
            {
                AudioSource.PlayClipAtPoint(gameOverSound, transform.position);
                gameOverSoundPlayed = true;
            }
        }
        Time.timeScale = 0f;
    }
    public void BackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("START SCREEN");
    }
}


