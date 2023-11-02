using UnityEngine;

public class fireBullet : MonoBehaviour
{
    public float flyingSpeed = 5f;

    private Transform player;
    private Rigidbody2D rb;


    private void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;
        
        rb = GetComponent<Rigidbody2D>();

        if (player == null)
        {
            Debug.LogError("Player not found!");
        }
        else
        {
            Vector2 directionToPlayer = (player.position - transform.position).normalized;
            rb.velocity = directionToPlayer * flyingSpeed;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Destroy the bullet when it collides with the player.
            Destroy(gameObject);
            //Deal damage to player!
        }
        else
        {
            Destroy(gameObject, 2.3f);
        }
    }

}