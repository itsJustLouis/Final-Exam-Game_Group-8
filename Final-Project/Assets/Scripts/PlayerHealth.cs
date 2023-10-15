using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    public int health; // Player's health
    public Slider healthBar; // Reference to the health bar Slider

    void Start()
    {
        // Initialize the health bar
        healthBar.maxValue = health;
        healthBar.value = health;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Health:" + health);
        // Check if the player has collided with an object tagged "explosion"
        if (collision.gameObject.tag == "Explosion")
        {
            // Decrease the player's health
            health -= 10;

            // Update the health bar
            healthBar.value = health;

            // Check if the player's health is 0 or less
            if (health <= 0)
            {
                // If so, destroy the player object
                //Destroy(gameObject);
            }
        }
    }
}