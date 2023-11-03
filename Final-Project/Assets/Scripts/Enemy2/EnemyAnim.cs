using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnim : MonoBehaviour
{
    private Vector2 lastPosition;
    public Rigidbody2D rb;
    public Animator anim;
    Vector2 currentPosition;
    public GameObject ExplosionObject;
    public EnemyHealth EnemyHealth;
    public GameObject Enemyparent;
    void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        lastPosition = rb.position;
        EnemyHealth = GetComponentInParent<EnemyHealth>();
        
    }

    void FixedUpdate()
    {
          Vector2 currentPosition = rb.position;
        Vector2 direction = currentPosition - lastPosition;

        if (direction != Vector2.zero)
        {
            direction.Normalize();
            Animate(direction);
        }

        lastPosition = currentPosition;
    }
    public void Explosion()
    {
        ExplosionObject.SetActive(true);
    }
    public void endExplosion()
    {
        ExplosionObject.SetActive(false);
        Enemyparent.SetActive(false);
        EnemyHealth.SpawnObject();
        EnemyHealth.DeactivateEnemy();
       
    }
    void Animate(Vector2 direction)
    {
        // Call your animation function here
        // For example, you might set animator parameters like this:
       anim.SetFloat("MovementX", direction.x);
        anim.SetFloat("MovementY", direction.y);


    }
}
