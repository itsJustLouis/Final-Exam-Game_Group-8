using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

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
    public float runSpeed = 7f;
    public float stamina;
    public float maxStamina;
    public bool isrunning;
    //public SwordAttack swordAttack;
    public Light2D FlashLight;
    public Light2D ConeLight;
    public InputAction accelerateAction;
    public FlashLight BatteryLevel;
    public GameObject DmgLightCollider;
    public GameObject LightCollider;
    public Slider StaminSlider;
    public float staminaDecreaseRate = 2f;
    public float staminaIncreaseRate = 1f;
    public float staminaRefillDelay = 2f;
    private bool isRunning = false;
    private bool isRefillingStamina = false;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer= GetComponent<SpriteRenderer>();
        stamina = maxStamina;
        MovementSpeedCache = MovementSpeed;
        BatteryLevel= GetComponentInChildren<FlashLight>();
        DmgLightCollider.active= false;
        LightCollider.active = true;

        StaminSlider.maxValue = stamina;
        StaminSlider.value = stamina;
    }
    // Start is called before the first frame update
    public void OnMove(InputValue inputValue)
    {

        movementInput = inputValue.Get<Vector2>();

      
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
        if (BatteryLevel.currentBatteryLevel>=1)
        {
            FlashLight.color = Color.red;
            Invoke("WhiteFlash", 0.5f);
            BatteryLevel.DecreaseBatteryLevel();

            DmgLightCollider.active = true;
            canMove = false;
            LightCollider.active = false;
        }
      
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

        if ((Input.GetKey(KeyCode.Space) || (gamepad != null && gamepad.buttonEast.isPressed)) && stamina > 0)
        {
            isrunning = true;
           MovementSpeed = runSpeed;
            stamina -= staminaDecreaseRate * Time.deltaTime; // decrease over time
        }
        else
        {
            isrunning = false;
            MovementSpeed = MovementSpeedCache;

            if (!isRefillingStamina && stamina < maxStamina)
            {
                StartCoroutine(RefillStaminaAfterDelay(staminaRefillDelay));
            }
        }

        // Ensure stamina stays within its bounds
        stamina = Mathf.Clamp(stamina, 0, maxStamina);
        // Ensure stamina stays within its bounds
        //  stamina = Mathf.Clamp(stamina, 0, maxStamina);

        Animate();
        StaminSlider.value = stamina;
        HandleFlashlight();

       
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
    IEnumerator StopRunningAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        IsNotRunning();
    }
    IEnumerator RefillStaminaAfterDelay(float delay)
    {
        isRefillingStamina = true;

        yield return new WaitForSeconds(delay);

        while (!isrunning && stamina < maxStamina)
        {
            stamina += staminaIncreaseRate * Time.deltaTime; // increase over time
            yield return null; // wait until next frame
        }

        isRefillingStamina = false;
    }
    void HandleFlashlight()
    {
        if (isrunning)
        {
            FlashLight.intensity = 0f;
            ConeLight.intensity = 0f;
            StopCoroutine("TurnOnLight");
        }
        else
        {
            // If the player is not running and the light is off, turn it back on after a delay
            if (FlashLight.intensity == 0f)
            {
                Invoke("TurnOnLight", 3f); // Wait for 3 seconds before turning on the light
            }
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
