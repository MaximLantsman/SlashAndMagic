using System.Collections.Generic;
using Entities;
using StateMechine;
using UnityEngine;
using Utilities;

public class EnemyBow : Enemy
{
    [SerializeField] private string enemyAttackAnimation;
    [SerializeField] private float timeBetweenAttacks = 2f;
    [SerializeField] private float hitStunDuration = 0.5f;
    [SerializeField] private float attackRange = 5f;
    
    [SerializeField] private Weapon currentWeapon;
    
    private StateMachine stateMachine;

    private List<Timer> timers;
    private CountdownTimer attackTimer;
    private CountdownTimer hitStunTimer;
    
    private bool isDead = false;
    
    public override void OnInitialized()
    {
        health.OnHit.AddListener(HitStun);
        
        SetUpTimers();

        SetUpStateMachine();
    }

    protected override void Update()
    {
        stateMachine.Update();

        HandleTimers();
    }

    private void HandleTimers()
    {
        foreach (Timer timer in timers)
        {
            timer.Tick(Time.deltaTime);
        }
    }
    
    protected override void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }

    protected override void SetUpTimers()
    {
        attackTimer = new CountdownTimer(timeBetweenAttacks);
        hitStunTimer= new CountdownTimer(hitStunDuration);


        timers = new List<Timer>(2) { attackTimer ,hitStunTimer };
    }

    protected override void SetUpStateMachine()
    {
        stateMachine = new StateMachine();

        //EnemyChaseState chaseState = new EnemyChaseState(this, animator, agent, player);
        EnemyAttackState attackState = new EnemyAttackState(this, animator, agent, Animator.StringToHash(enemyAttackAnimation),currentWeapon);

        EnemyRunState runState = new EnemyRunState(this, animator, agent, player);
        
        EnemyHitStunState hitStunState = new EnemyHitStunState(this, animator);
        EnemyDeathState deathState = new EnemyDeathState(this, animator);

        At(attackState, runState, new FuncPredicate(() => !CanAttackPlayer()));

        At(runState, attackState, new FuncPredicate(() => CanAttackPlayer()));

        
        Any(attackState, new FuncPredicate(() => !attackTimer.IsRunning &&  !CanAttackPlayer()  &&  !hitStunTimer.IsRunning && !isDead));
        
        Any(hitStunState,new FuncPredicate(() => hitStunTimer.IsRunning));
        Any(deathState,new FuncPredicate(() => isDead));
        
        attackTimer.OnTimerStop += () =>
        {
            
        };

        stateMachine.SetState(attackState);
    }

    private void At(IState from, IState to, IPredicate condition) => stateMachine.AddTransition(from, to, condition);
    private void Any(IState to, IPredicate condition) => stateMachine.AddAnyTransition(to, condition);

    public override void Attack()
    {
        if(attackTimer.IsRunning) return;

        attackTimer.Start();
        currentWeapon.Attack();
    }

    protected override void HitStun()
    {
        if (health.IsDead)
        {
            isDead = true;
            return;
        }
        
        hitStunTimer.Start();
    }


    private bool CanAttackPlayer()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        return directionToPlayer.magnitude > attackRange;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,attackRange);
    }
}