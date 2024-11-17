using UnityEngine;
using UnityEngine.AI;

public class EnemyAttackState : EnemyBaseState
{
    private readonly NavMeshAgent agent;

    public EnemyAttackState(Enemy enemy, Animator animator, NavMeshAgent agent, Transform player) : base(enemy, animator)
    {
        this.agent = agent;
    }

    public override void OnEnter()
    {
        agent.SetDestination(enemy.transform.position);
        animator.CrossFade(AttackHash, crossFadeDuration);
        enemy.HandleRotation();
    }

    public override void Update()
    {
        Debug.Log("attacking");
        enemy.Attack();
    }
}