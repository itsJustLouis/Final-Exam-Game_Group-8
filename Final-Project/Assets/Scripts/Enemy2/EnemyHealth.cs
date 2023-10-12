using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public int health = 100;
    private Animator animator;
    private Rigidbody2D rb;
    public float knockbackForce;
    void Start()
    {
        animator = GetComponentInChildren<Animator>();

        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int damage, Vector3 hitDirection)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }else
        {
            // Apply a force to the enemy in the opposite direction of the hit
            GetComponent<Rigidbody2D>().AddForce(-hitDirection.normalized * knockbackForce, ForceMode2D.Impulse);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collided object is the trigger
        if (other.gameObject.CompareTag("Light"))
        {
            //canMove = true;

        }
        else
        {
            if (other.gameObject.CompareTag("DmgLight"))
            {

                Vector3 hitDirection = other.transform.position - transform.position;
                //reduce health here
                TakeDamage(1, hitDirection);
               // canMove = false;
               // Invoke("CanMoveAgain", 1f);
            }
        }
    }
    void Die()
    {
        // Play the death animation
        animator.SetTrigger("Blow");

        // Set the Rigidbody to static
        rb.bodyType = RigidbodyType2D.Static;
        // Disable the enemy script (or destroy the object, etc.)
        // This is just an example, you might want to do something different when the enemy dies
       /// gameObject.SetActive(false);
        //this.enabled = false;
    }
   public void DeactivateEnemy()
    {
        if (health<=0)
        {
            gameObject.SetActive(false);
        }
    }
    private void Update()
    {

        if (health <= 0)
        {
            Die();
        }
    }
}
