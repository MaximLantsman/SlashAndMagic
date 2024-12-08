using Damagable;
using Entities;
using KBCore.Refs;
using StateMechine;
using UnityEngine;
using UnityEngine.AI;
using Utilities;

public abstract class Enemy : Entity
{
    [SerializeField, Self] public NavMeshAgent agent;
    [SerializeField, Self] public Animator animator;
    [SerializeField, Self] public Collider enemyCollider;
    [SerializeField, Self] public Health.Health health;
    public Transform player;
    
    private void OnValidate() => this.ValidateRefs();
    
    public abstract void OnInitialized();
    protected abstract void Update();

    protected abstract void FixedUpdate();
    
    protected abstract void SetUpTimers();

    protected abstract void SetUpStateMachine();

    public abstract void Attack();
    
    public void HandleRotation()
    {
        transform.LookAt(player.position);
    }
    
    protected abstract void HitStun();
    //protected abstract void EnemyDeath();
}


