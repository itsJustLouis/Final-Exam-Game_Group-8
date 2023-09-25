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
    private Vector2 pointerinput;
    private Weaponrotate weaponrotate;
    [SerializeField]
    private InputActionReference Look;


    //public SwordAttack swordAttack;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer= GetComponent<SpriteRenderer>();
        weaponrotate = GetComponentInChildren<Weaponrotate>();
    }
    // Start is called before the first frame update
    public void OnMove(InputValue inputValue)
    {

        Vector2 inputVector = inputValue.Get<Vector2>();
        MovementInput = inputVector;
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
    private Vector2 GetPointerInput()
    {
        Vector3 mousePos = Look.action.ReadValue<Vector2>();
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
    private void Update()
    {
        pointerinput = GetPointerInput();
        weaponrotate.Pointerposition = pointerinput;

     }
}
