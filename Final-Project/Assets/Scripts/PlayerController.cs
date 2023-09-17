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
    //public SwordAttack swordAttack;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer= GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    public void OnMove(InputValue inputValue)
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
           // spriteRenderer.flipX = true;
            
        }
        else if (inputVector.x > 0)
        {
            // Flip the sprite to the right.
           // spriteRenderer.flipX = false;
           
        }

        if (inputVector.y < 0)
        {
            //flip attack box up
            
        }
        else if(inputVector.y > 0)
        {
            //flip attack box down
          
        }
    }
    public void Attack()
    {
        if (spriteRenderer.flipX==true)
        {
            // Flip the sprite to the left.
            //swordAttack.attackleft();
           
        }
        else if (spriteRenderer.flipX==false)
        {
            // Flip the sprite to the right.
            //swordAttack.attackright();
            
        }

        if (MovementInput.y < 0)
        {
            //flip attack box up
            //swordAttack.attackup();
        }
        else if (MovementInput.y > 0)
        {
            //flip attack box down
            //swordAttack.attackdown();
        }
    }
    public void stopattack()
    {
        //swordAttack.stopattack();
    }
    private void Animate()
    {
        if (Mathf.Abs(MovementInput.x)>Mathf.Abs(MovementInput.y))
        {
            anim.SetFloat("Movement_X", MovementInput.x* (1/Mathf.Abs(MovementInput.x)));
            anim.SetFloat("Movement_Y", 0);
        }
        else
        {
            anim.SetFloat("Movement_X", 0);
            anim.SetFloat("Movement_Y", MovementInput.y * (1/Mathf.Abs(MovementInput.y)));  
        }
       // anim.SetFloat("Movement_X", MovementInput.x);
       // anim.SetFloat("Movement_Y", MovementInput.y);
        
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
