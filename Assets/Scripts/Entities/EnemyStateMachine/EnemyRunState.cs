using UnityEngine;
using UnityEngine.AI;

public class EnemyRunState : EnemyBaseState
{
    private readonly NavMeshAgent agent;
    private readonly Transform playerPosition;

    
    public EnemyRunState(Enemy enemy, Animator animator, NavMeshAgent agent,Transform player) : base(enemy, animator)
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
        //agent.SetDestination(playerPosition.position);
        agent.SetDestination(agent.gameObject.transform.position - playerPosition.position);
    }
    
   
}