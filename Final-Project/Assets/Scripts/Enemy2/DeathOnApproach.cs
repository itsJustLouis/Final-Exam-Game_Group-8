using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathOnApproach : MonoBehaviour
{
    public float detectionRadius = 5.0f; // The radius within which the player is detected
    public Animator animator; // Reference to the Animator component
   
    private CircleCollider2D circleCollider; // Reference to the CircleCollider2D component
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        // Add a sphere collider as a trigger
        // Add a circle collider as a trigger
        circleCollider = gameObject.AddComponent<CircleCollider2D>();
        circleCollider.isTrigger = true;
        circleCollider.radius = detectionRadius;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player has entered the trigger
        if (other.gameObject.tag == "Player")
        {
            // If so, play the death animation
            animator.SetTrigger("Blow");
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw a wire sphere at the position of the game object with a radius equal to detectionRadius
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, circleCollider.radius);
    }

}
