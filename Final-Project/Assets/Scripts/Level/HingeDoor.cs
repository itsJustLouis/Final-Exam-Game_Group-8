using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HingeDoor : MonoBehaviour
{
    public Color glowColor = Color.yellow;
    public float doorSpeed = 5.0f;

    private HingeJoint2D hj;
    private SpriteRenderer doorRenderer;

    private void Start()
    {
        hj = transform.Find("hinge").GetComponent<HingeJoint2D>();
        doorRenderer = transform.Find("door").GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        bool isPlayerColliding = false;

        if (doorRenderer.color == glowColor)
        {
            Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, doorRenderer.bounds.size, 0);
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Player"))
                {
                    isPlayerColliding = true;
                    break;
                }
            }

            hj.useMotor = isPlayerColliding;
        }
        else
        {
            hj.useMotor = false;
        }

        if (hj.useMotor)
        {
            JointMotor2D motor = hj.motor;
            motor.motorSpeed = doorSpeed;
            hj.motor = motor;
        }
    }
}
