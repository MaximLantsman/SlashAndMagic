using System;
using Damagable;
using Entities;
using KBCore.Refs;
using StateMechine;
using UnityEngine;
using UnityEngine.AI;
using Utilities;

public class Enemy : Entity, IDamagable
{
    [SerializeField, Self]private NavMeshAgent agent;
    [SerializeField, Self]private Animator animator;
    [SerializeField, Anywhere] private Transform player;
    
    [SerializeField]private int maxhealth = 100;
    [SerializeField]private float timeBetweenAttacks = 2f;
    [SerializeField]private float timeBetweenBattleCry = 2f;
    [SerializeField]private Weapon currentWeapon;


    [SerializeField]private float attackRange = 2f;

    private int currentHealth;
    private StateMachine stateMachine;
    private CountdownTimer attackTimer;
    private CountdownTimer battleCryTimer;

    private void OnValidate() => this.ValidateRefs();

    private void Start()
    {
        attackTimer = new CountdownTimer(timeBetweenAttacks);
        battleCryTimer = new CountdownTimer(timeBetweenBattleCry);
        currentHealth = maxhealth;
        stateMachine = new StateMachine();

        EnemyChaseState chaseState = new EnemyChaseState(this, animator, agent, player);
        EnemyAttackState attackState = new EnemyAttackState(this, animator, agent, player);
        EnemyBattleCryState battleCryState = new EnemyBattleCryState(this,animator, agent, player);
        
        At(chaseState, attackState, new FuncPredicate(() => CanAttackPlayer()));
        
        At(attackState, chaseState, new FuncPredicate(() => !attackTimer.IsRunning && !CanAttackPlayer()));
        At(attackState, battleCryState, new FuncPredicate(() => battleCryTimer.IsRunning));
        
        At(battleCryState, attackState, new FuncPredicate(() =>!battleCryTimer.IsRunning && CanAttackPlayer())) ;
        At(battleCryState, chaseState, new FuncPredicate(() =>!battleCryTimer.IsRunning &&  !CanAttackPlayer())) ;

        attackTimer.OnTimerStop += () =>
        {
            if (CanAttackPlayer())
            {
                battleCryTimer.Start();
            }
        };

        stateMachine.SetState(chaseState);
    }
    
    private void At(IState from,IState to, IPredicate condition)=> stateMachine.AddTransition(from,to,condition);
    private void Any(IState to, IPredicate condition)=> stateMachine.AddAnyTransition(to,condition);

    private void Update()
    {
      
        stateMachine.Update();
        
        
        attackTimer.Tick(Time.deltaTime);
        battleCryTimer.Tick(Time.deltaTime);
    }

    private void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }
    
    public void HandleRotation()
    {
        transform.LookAt(player.position);
    }
    
    public void Damage(int damageAmount)
    {
        currentHealth-=damageAmount;
        Debug.Log(currentHealth);
    }

    public void Attack()
    {
        if(attackTimer.IsRunning) return;

        attackTimer.Start();
        currentWeapon.Attack();
    }
    
    private bool CanAttackPlayer()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        return directionToPlayer.magnitude < attackRange;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,attackRange);
    }
}


