using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireBullet : MonoBehaviour
{
    public Transform player;
    public float flyingSpeed = 10f;

    private Rigidbody2D rb;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(EnemyBehavior());
    }

    private IEnumerator EnemyBehavior()
    {
        Vector2 targetPosition = player.position;

        while (true)
        {
            // Update direction to the target position (player's position)
            Vector2 directionToTarget = (targetPosition - (Vector2)transform.position).normalized;

            // Fly towards the target position using Rigidbody2D
            rb.velocity = directionToTarget * flyingSpeed;
        }
    }
}

