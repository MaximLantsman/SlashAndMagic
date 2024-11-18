using Damagable;
using Entities;
using KBCore.Refs;
using StateMechine;
using UnityEngine;
using UnityEngine.AI;
using Utilities;

public abstract class Enemy : Entity, IDamagable
{
    [SerializeField, Self] public NavMeshAgent agent;
    [SerializeField, Self] public Animator animator;
    [SerializeField, Anywhere] public Transform player;
    
    [SerializeField]private int maxhealth = 100;
    
    private int currentHealth;
    
    private void OnValidate() => this.ValidateRefs();

    private void Start()
    {
        currentHealth = maxhealth;
    }
    
    protected abstract void Update();

    protected abstract void FixedUpdate();
    
    protected abstract void SetUpTimers();

    protected abstract void SetUpStateMachine();

    public abstract void Attack();
    
    public void HandleRotation()
    {
        transform.LookAt(player.position);
    }
    
    public void Damage(int damageAmount)
    {
        currentHealth-=damageAmount;
        Debug.Log(currentHealth);
    }
    
}


