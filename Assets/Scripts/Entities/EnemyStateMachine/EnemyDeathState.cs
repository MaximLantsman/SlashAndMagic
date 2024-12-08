using UnityEngine;

public class EnemyDeathState : EnemyBaseState
{
    public EnemyDeathState(Enemy enemy, Animator animator) : base(enemy, animator)
    {

    }

    public override void OnEnter()
    {
        animator.CrossFade(DeathHash, crossFadeDuration);
        Debug.Log("Death");
    }
    

    public override void Update()
    {

    }
}