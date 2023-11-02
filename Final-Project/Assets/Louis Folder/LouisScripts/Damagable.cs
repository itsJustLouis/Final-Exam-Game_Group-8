using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damagable : MonoBehaviour
{
   
    public int MaxHealth = 80;
    public int damage = 15;
    private float lastDamageTime;
    public GameObject enemy;
    public Canvas healthCanva;
    public Animator animator; 

    [SerializeField]
    private int health;

    public int Health
    {
        get { return health; }
        set
        {
            health = value;
            OnHealthChange?.Invoke((float)Health / MaxHealth);
        }
    }


    public UnityEvent OnDead;
    public UnityEvent<float> OnHealthChange;
    public UnityEvent OnHit, OnHeal;


    private void Start()
    {
        Health = MaxHealth;
        animator = GetComponent<Animator>();
    }

  

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "DmgLight")
        {
            // Check if enough time has passed since the last damage
            if (Time.time - lastDamageTime >= 0.5f)
            {
                Health -= damage;
                lastDamageTime = Time.time; // Update the last damage time
            }
        }
        if (collision.gameObject.tag == "Light")
        {
            // Check if enough time has passed since the last damage
            if (Time.time - lastDamageTime >= 0.5f)
            {
                Health -= 15;
                lastDamageTime = Time.time; // Update the last damage time
            }
        }
    }
    public void Update()
    {

        if (Health <= 0)
        {
            animator.SetBool("Blow", true);
            
            StartCoroutine(waitAbit());
            
        }
        else
        {
            OnHit?.Invoke();
            //Debug.Log("STILL HERE"); //Enemy is still alive
        }
    }
     IEnumerator waitAbit()
    {
        yield return new WaitForSeconds(1.5f);
        healthCanva.gameObject.SetActive(false);
        Destroy(enemy);
    }
    public void ResetHealth()
    {
        Health = MaxHealth;
    }
    
}
