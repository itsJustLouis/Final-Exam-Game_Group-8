using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
   
   
    Vector2 downAttackOffset;
    Collider2D swordCollider;

    private void Start()
    {
        downAttackOffset = transform.position;
        swordCollider = GetComponent<Collider2D>();
    }


   

    public void attackdown()
    {
        Debug.Log("Attack Down");
        swordCollider.enabled = true;
        transform.position = downAttackOffset;
        
    }

    public void attackup()
    {
        Debug.Log("Attack Up");
        swordCollider.enabled = true;
        transform.position = new Vector3(downAttackOffset.x, downAttackOffset.y * 1);
       
    }

    public void stopattack()
    {
        swordCollider.enabled = false;
       
    }
}
