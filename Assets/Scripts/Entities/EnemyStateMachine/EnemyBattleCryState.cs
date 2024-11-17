using UnityEngine;
using UnityEngine.AI;

public class EnemyBattleCryState : EnemyBaseState
{
    private readonly NavMeshAgent agent;
    private readonly Transform playerPosition;
    
    
    
    public EnemyBattleCryState(Enemy enemy, Animator animator, NavMeshAgent agent,Transform player) : base(enemy, animator)
    {
        this.agent = agent;
        this.playerPosition = player;
    }

    public override void OnEnter()
    {
        animator.CrossFade(BattleCryHash, crossFadeDuration);
    }

    public override void Update()
    {
       enemy.HandleRotation();
    }
    
   
}