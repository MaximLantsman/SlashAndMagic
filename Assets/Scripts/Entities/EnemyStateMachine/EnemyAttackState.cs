using Entities;
using UnityEngine;
using UnityEngine.AI;


public class EnemyAttackState : EnemyBaseState
{
    private readonly NavMeshAgent agent;
    private int weaponAnimation;
    private Weapon curWeapon;
    
    public EnemyAttackState(Enemy enemy, Animator animator, NavMeshAgent agent, int newAttackHash,Weapon weaponAttack) : base(enemy, animator)
    {
        weaponAnimation = newAttackHash;
        curWeapon = weaponAttack;
        this.agent = agent;
    }

    public override void OnEnter()
    {
        
        agent.SetDestination(enemy.transform.position);
        
        animator.CrossFade(weaponAnimation, crossFadeDuration);
        enemy.HandleRotation();

    }
    

    public override void Update()
    {
        enemy.HandleRotation();
        enemy.Attack();
    }
}