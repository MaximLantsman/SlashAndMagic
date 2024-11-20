using System.Collections.Generic;
using Entities;
using StateMechine;
using UnityEngine;
using Utilities;

public class EnemyDagger : Enemy
{
    [SerializeField]private string enemyAttackAnimation;
    [SerializeField]private float timeBetweenAttacks = 2f;
    [SerializeField]private float attackRange = 2f;

    
    [SerializeField]private Weapon currentWeapon;
    
    private StateMachine stateMachine;

    private List<Timer> timers;
    private CountdownTimer attackTimer;

    public override void OnInitialized()
    {
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

        timers = new List<Timer>(1) { attackTimer };
    }

    protected override void SetUpStateMachine()
    {
        stateMachine = new StateMachine();

        EnemyChaseState chaseState = new EnemyChaseState(this, animator, agent, player);
        EnemyAttackState attackState = new EnemyAttackState(this, animator, agent, Animator.StringToHash(enemyAttackAnimation),currentWeapon);
        EnemyBattleCryState battleCryState = new EnemyBattleCryState(this, animator, agent, player);

        At(chaseState, attackState, new FuncPredicate(() => CanAttackPlayer()));

        At(attackState, chaseState, new FuncPredicate(() => !attackTimer.IsRunning && !CanAttackPlayer()));

        At(attackState, attackState, new FuncPredicate(() => CanAttackPlayer()));
        
        attackTimer.OnTimerStop += () =>
        {
            if (CanAttackPlayer())
            {
                Attack();
            }
        };

        stateMachine.SetState(chaseState);
    }

    private void At(IState from, IState to, IPredicate condition) => stateMachine.AddTransition(from, to, condition);
    private void Any(IState to, IPredicate condition) => stateMachine.AddAnyTransition(to, condition);

    public override void Attack()
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