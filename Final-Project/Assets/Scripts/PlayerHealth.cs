using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    public int health; // Player's health
    public Slider healthBar; // Reference to the health bar Slider
    public GameObject lowHealth;
    public GameObject deathPanel;
    void Start()
    {
        lowHealth.SetActive(false);
        deathPanel.SetActive(false);
        // Initialize the health bar
        healthBar.maxValue = health;
        healthBar.value = health;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Explosion" || collision.gameObject.tag == "strike" || collision.gameObject.tag == "slash")
        {
            // Decrease the player's health
            health -= 10;

            // Update the health bar
            healthBar.value = health;

            if (health <= 0)
            {
                Debug.Log("Player DEAD");
                lowHealth.SetActive(false);
                deathPanel.SetActive(true);
                StartCoroutine(Wait());
            }
            else if (health <= 20)
            {
                lowHealth.SetActive(true);
            }
            else
            {
                lowHealth.SetActive(false);
            }
        }
        if (collision.gameObject.CompareTag("Health"))
        {
            //increase player health
            health = ((int)healthBar.maxValue);
            healthBar.value = health;
            collision.gameObject.SetActive(false);
        }


    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}