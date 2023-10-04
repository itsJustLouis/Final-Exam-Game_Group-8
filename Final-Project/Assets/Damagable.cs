using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damagable : MonoBehaviour
{
   
    public int MaxHealth = 100;
    public GameObject enemy;
    public Canvas healthCanva;

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
       
    }

    //internal void Hit(int damagePoints)
    //{
    //    Health -= damagePoints;
    //    if (Health <= 0)
    //    {
    //        OnDead?.Invoke();
    //    }
    //    else
    //    {
    //        OnHit?.Invoke();
    //    }
    //}
    public void OnCollisionEnter2D(Collision2D hitInfo)
    {

        if (hitInfo.gameObject.tag == "bullet")
        {
            Health -= 10;
           

        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            Health -= 10;
        }
    }
    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Health -= 10;
        }
        if (Health <= 0)
        {
            OnDead?.Invoke();
            // Debug.Log("DEAD");  //Enemy is dead

            //m_Animator.GetComponent<Animator>().enabled = true;
            //m_Animator.Play("exploding");

            healthCanva.gameObject.SetActive(false);
            enemy.SetActive(false);
        }
        else
        {
            OnHit?.Invoke();
            //Debug.Log("STILL HERE"); //Enemy is still alive
        }
    }

    public void ResetHealth()
    {
        Health = MaxHealth;
    }

    public void Heal(int healthBoost)
    {
        Health += healthBoost;
        Health = Mathf.Clamp(Health, 0, MaxHealth);
        OnHeal?.Invoke();
    }
}