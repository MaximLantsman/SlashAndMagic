using UnityEngine;
using UnityEngine.AI;

public class EnemyChaseState : EnemyBaseState
{
    private readonly NavMeshAgent agent;
    private readonly Transform playerPosition;

    
    public EnemyChaseState(Enemy enemy, Animator animator, NavMeshAgent agent,Transform player) : base(enemy, animator)
    {
        this.agent = agent;
        playerPosition = player;
    }

    public override void OnEnter()
    {
        animator.CrossFade(RunHash, crossFadeDuration);
    }

    public override void Update()
    {
        enemy.HandleRotation();
        agent.SetDestination(playerPosition.position);
    }
    
   
}