using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralHeath : MonoBehaviour, IHealth
{
    [SerializeField] float maxHealth = 15f;
    private float health;

    public float Health => health;
    public float MaxHealth => maxHealth;

    public event Action OnDie;
    public event Action<float> OnTakeDamage;

    private void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (health <= damage)
        {
            health = 0;
            OnTakeDamage.Invoke(health);
            OnDie.Invoke();
        }
        else
        {
            health -= damage;
            OnTakeDamage.Invoke(health);
        }

    }
}
