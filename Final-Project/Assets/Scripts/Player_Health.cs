using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player_Health : MonoBehaviour
{
    public int MaxHealth = 100;
    public int SlashDamage;
    public int StrikeDamage;
    public GameObject Player;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "slash")
        {     
                Health -= SlashDamage;            
        }
        if (collision.gameObject.tag == "strike")
        {
            Health -= StrikeDamage;
        }
    }
    public void Update()
    {
        if (Health <= 0)
        {
            OnDead?.Invoke();
            // Debug.Log("DEAD");  //Enemy is dead

            //m_Animator.GetComponent<Animator>().enabled = true;
            //m_Animator.Play("exploding");

            healthCanva.gameObject.SetActive(false);
            Player.SetActive(false);
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

