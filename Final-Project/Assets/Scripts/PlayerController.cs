using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    public float MovementSpeed;
    private Vector2 MovementInput;
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer= GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    private void OnMove(InputValue inputValue)
    {

        Vector2 inputVector = inputValue.Get<Vector2>();

        if (inputVector == Vector2.zero)
        {
            // If the input vector is zero (no input), stop the player's movement.
            rb.velocity = Vector2.zero;
        }
        else
        {
            // If there is input, update the player's movement based on the input.
            rb.velocity = new Vector2(inputVector.x * MovementSpeed, inputVector.y * MovementSpeed);
        }

        // Store the input for other purposes if needed.
        MovementInput = inputVector;

        if (inputVector.x < 0)
        {
            // Flip the sprite to the left.
            spriteRenderer.flipX = true;
        }
        else if (inputVector.x > 0)
        {
            // Flip the sprite to the right.
            spriteRenderer.flipX = false;
        }
    }
    private void Animate()
    {
        anim.SetFloat("Movement_X", MovementInput.x);
        anim.SetFloat("Movement_Y", MovementInput.y);
        
    }
    private void OnFire()
    {
        anim.SetTrigger("Attack");
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Animate();
        
             rb.velocity = MovementInput * MovementSpeed;  
       
    }
}
