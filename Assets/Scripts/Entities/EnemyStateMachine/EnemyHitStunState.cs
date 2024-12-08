using UnityEngine;

public class EnemyHitStunState : EnemyBaseState
{

    
    public EnemyHitStunState(Enemy enemy, Animator animator) : base(enemy, animator)
    {

    }

    public override void OnEnter()
    {
        Debug.Log("hitstun");
        animator.CrossFade(HitStunHash, crossFadeDuration);
    }
    

    public override void Update()
    {

    }
}