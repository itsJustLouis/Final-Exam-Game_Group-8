using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    public float MovementSpeed;
    public float MovementSpeedCache;
    private Vector2 movementInput;
    private SpriteRenderer spriteRenderer;
    public ContactFilter2D movementFilter;
    public float collisionOffset = 0.05f;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    bool canMove = true;
    public float speed;
    public float stamina;
    public float maxStamina;
    public bool isrunning;
    //public SwordAttack swordAttack;
    public Light2D FlashLight;
    public Light2D ConeLight;
    public InputAction accelerateAction;
    public Battery Battery;
    public GameObject DmgLightCollider;
    public GameObject LightCollider;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer= GetComponent<SpriteRenderer>();
        stamina = maxStamina;
        MovementSpeedCache = MovementSpeed;
        Battery= GetComponentInChildren<Battery>();
        DmgLightCollider.active= false;
        LightCollider.active = true;


    }
    // Start is called before the first frame update
    public void OnMove(InputValue inputValue)
    {

        movementInput = inputValue.Get<Vector2>();

      
    }
    public void OnAccelerate()
    {
        if (stamina > 0)
        {
            IsRunning();
            //MovementSpeed = 5;
            stamina -= Time.deltaTime; // decrease over time
           
        }
        else
        {
            // If Space is not pressed or stamina is empty, return speed to normal and gradually refill stamina
         //   MovementSpeed = 2;
            if (stamina < maxStamina)
            {
                stamina += Time.deltaTime; // increase over time
            }
            isrunning = false;

            // Ensure stamina stays within its bounds
            stamina = Mathf.Clamp(stamina, 0, maxStamina);
        }

    }
    public bool IsRunning()
    {
        
            isrunning = true;

       /// StopCoroutine("TurnOnLight");

        return isrunning;
    }
   public bool IsNotRunning()
    {
        isrunning = false;
        return isrunning;
    }
    public void OnAccelerateCanceled()
    {
        // If Space is not pressed or stamina is empty, return speed to normal and gradually refill stamina
        MovementSpeed = 2;
        if (stamina < maxStamina)
        {
            stamina += Time.deltaTime; // increase over time
        }
        isrunning = false;

        // Ensure stamina stays within its bounds
        stamina = Mathf.Clamp(stamina, 0, maxStamina);
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
        FlashLight.color = Color.red;
        Invoke("WhiteFlash", 0.5f);
        Battery.DecreaseAmount();
        Battery.isRegenerating = false;
        DmgLightCollider.active = true;
       canMove = false;
        LightCollider.active = false;
    }
    public void WhiteFlash()
    {
        FlashLight.color = Color.white;
        DmgLightCollider.active=false;
        LightCollider.active = true;
        canMove=true;
    }
    // Update is called once per frame
    void Update()
    {

        Gamepad gamepad = Gamepad.current; //get the current gamepad
        if ((Input.GetKey(KeyCode.Space) || (gamepad != null && gamepad.buttonEast.isPressed)) && stamina > 1)
        {
            IsRunning();
            isrunning = true;
            MovementSpeed = 7;
            stamina -= Time.deltaTime; // decrease over time
        }
        else
        {
            // If Space is not pressed or stamina is empty, return speed to normal and gradually refill stamina
          MovementSpeed = MovementSpeedCache;
            if (stamina < maxStamina)
            {
                stamina += Time.deltaTime; // increase over time
            }
            //delay on running
            Invoke("IsNotRunning", 1f);
        }

        // Ensure stamina stays within its bounds
        stamina = Mathf.Clamp(stamina, 0, maxStamina);

        Animate();


        if (isrunning)
        {
            FlashLight.intensity = 0f;
            ConeLight.intensity = 0f;
            StopCoroutine("TurnOnLight");
        }
        else
        {
            // If the player is not running and the light is off, turn it back on after a delay
            if (FlashLight.intensity == 0f && !isrunning)
            {
                Invoke("TurnOnLight", 3f); // Wait for 3 seconds before turning on the light
            }
        }
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
    void TurnOnLight()
    {
        if (!isrunning)
        {
            FlashLight.intensity = 2f;
            ConeLight.intensity = 2f;
        }
        else
        {
            FlashLight.intensity = 0f;
            ConeLight.intensity = 0f;
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
