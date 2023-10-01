using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    public float MovementSpeed;
    private Vector2 movementInput;
    private SpriteRenderer spriteRenderer;
    public ContactFilter2D movementFilter;
    public float collisionOffset = 0.05f;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    bool canMove = true;

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

        movementInput = inputValue.Get<Vector2>();

      
    }
    public void Attack()
    {
       
    }
    public void stopattack()
    {
        //swordAttack.stopattack();
    }
    private void Animate()
    {
        if (Mathf.Abs(movementInput.x)>Mathf.Abs(movementInput.y))
        {
            anim.SetFloat("Movement_X", movementInput.x* (1/Mathf.Abs(movementInput.x)));
            anim.SetFloat("Movement_Y", 0);
        }
        else
        {
            anim.SetFloat("Movement_X", 0);
            anim.SetFloat("Movement_Y", movementInput.y * (1/Mathf.Abs(movementInput.y)));  
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

        if (canMove)
        {
            if (movementInput != Vector2.zero)
            {
                bool success = TryMove(movementInput);

                if (!success)
                {
                    success = TryMove(new Vector2(movementInput.x, 0));


                    if (!success)
                    {
                        success = TryMove(new Vector2(0, movementInput.y));
                    }
                }
              

            }
         

            //set direction of sprite to movement direction


            
        }

    }

    private bool TryMove(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            int count = rb.Cast(direction, movementFilter, castCollisions, MovementSpeed * Time.fixedDeltaTime + collisionOffset);
            if (count == 0)
            {
                rb.MovePosition(rb.position + direction * MovementSpeed * Time.fixedDeltaTime);
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }



    }

    public void LockMovement()
    {
        canMove = false;
    }

    public void UnlockMovemnt()
    {
        canMove = true;
    }

}
