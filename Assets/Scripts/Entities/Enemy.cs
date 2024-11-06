using System;
using Damagable;
using Entities;
using UnityEngine;

public class Enemy : Entity, IDamagable
{
    [SerializeField]private int maxhealth = 100;
    
    private int currentHealth;

    private void Start() => currentHealth = maxhealth;

    public void Damage(int damageAmount)
    {
        currentHealth-=damageAmount;
        Debug.Log(currentHealth);
    }
}
