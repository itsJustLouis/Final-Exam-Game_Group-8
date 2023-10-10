using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_Health : MonoBehaviour
{
     void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag=="Player")
        {
            Debug.Log("Collided with Player.");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="Hitbox")
        {
            Debug.Log("Collided with a Hitbox object.");
        }
    }
}
