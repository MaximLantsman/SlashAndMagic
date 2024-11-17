using System;
using StateMechine;
using UnityEngine;

public abstract class EnemyBaseState: IState
{
    private protected readonly Enemy enemy;
    private protected readonly Animator animator;

    private protected const float crossFadeDuration = 0.1f;

    protected static readonly int IdleHash = Animator.StringToHash("Idle");
    protected static readonly int RunHash = Animator.StringToHash("Run");
    protected static readonly int BattleCryHash = Animator.StringToHash("BattleCry");
    protected static readonly int AttackHash = Animator.StringToHash("WideSwing");


    
    private protected EnemyBaseState(Enemy enemy, Animator animator)
    {
        this.enemy = enemy;
        this.animator = animator;
    }
    
    public virtual void OnEnter()
    {
        throw new NotImplementedException();
    }

    public virtual void Update()
    {
        throw new NotImplementedException();
    }

    public virtual void FixedUpdate()
    {
        //noop
    }

    public virtual void OnExit()
    {
        //noop
    }
}