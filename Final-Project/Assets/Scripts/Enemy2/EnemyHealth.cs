using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public int health = 100;
    private Animator animator;
    private Rigidbody2D rb;
    public float knockbackForce;
    public Color flashColor1 = Color.red;
    public Color flashColor2 = Color.blue;
    public bool isMoving = true;
    public GameObject spawnObject1;
    public GameObject spawnObject2;
    private GameObject lastSpawned;
    public Slider healthBar;
    void Start()
    {
        animator = GetComponentInChildren<Animator>();

        rb = GetComponent<Rigidbody2D>();

        healthBar.maxValue= health;
        healthBar.value= health;


    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        Debug.Log("Taking " + damage + " damage");


        if (health <= 0)
        {
            Die();
            healthBar.value = health;

        }
        else
        {
            // Freeze the enemy
            rb.bodyType = RigidbodyType2D.Static;

            // Start the color flashing coroutine
            StartCoroutine(FlashColor());


            // After a few seconds, unfreeze the enemy and stop flashing
            Invoke("Unfreeze", 3f);

            healthBar.value = health;
            // Apply a force to the enemy in the opposite direction of the hit
            // GetComponent<Rigidbody2D>().AddForce(-hitDirection.normalized * knockbackForce, ForceMode2D.Impulse);

        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collided object is the trigger
        if (other.gameObject.CompareTag("Light"))
        {
            //canMove = true;
            Debug.Log("Trigger entered with " + other.gameObject.tag);
        }
        else
        {
            if (other.gameObject.CompareTag("DmgLight"))
            {





               /// Vector3 hitDirection = other.transform.position - transform.position;
                //reduce health here
                TakeDamage(1);
               // canMove = false;
               // Invoke("CanMoveAgain", 1f);
            }
        }
    }
    void Die()
    {
        // Play the death animation
        animator.SetTrigger("Blow");

        // Spawn a new object at the enemy's position
        //SpawnObject();



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

    IEnumerator FlashColor()
    {
        SpriteRenderer renderer = GetComponentInChildren<SpriteRenderer>();
        bool useFirstColor = true;

        while (rb.bodyType == RigidbodyType2D.Static)
        {
            // Change to the appropriate color
            renderer.color = useFirstColor ? flashColor1 : flashColor2;
            useFirstColor = !useFirstColor;

            // Wait for a short time
            yield return new WaitForSeconds(0.1f);
        }

        // Change back to the original color
        renderer.color = Color.white;
    }

    void Unfreeze()
    {
        if (health > 0)
        {
            // Unfreeze the enemy
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }
    // Call this method to resume enemy movement
    public void ResumeMoving()
    {
        isMoving = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
    public void StopMoving()
    {
        // Set isMoving to false
        isMoving = false;

        // Stop the enemy from moving
        // This depends on how you've implemented enemy movement
        // For example, if you're using a Rigidbody2D to move the enemy, you can do:
        rb.bodyType= RigidbodyType2D.Static;
    }
   public void SpawnObject()
    {
        GameObject toSpawn;

        do
        {
            // Choose a random object to spawn
            toSpawn = Random.value < 0.5f ? spawnObject1 : spawnObject2;
        } while (toSpawn == lastSpawned);

        lastSpawned = toSpawn;

        // Instantiate the new object at this enemy's position
        Instantiate(toSpawn, transform.position, Quaternion.identity);
    }



}
