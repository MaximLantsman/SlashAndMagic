using StateMechine;
using UnityEngine;

public class HitStunState : BaseState
{
    public HitStunState(PlayerController player, Animator animator) : base(player, animator) { }
    
    public override void OnEnter()
    {
        animator.CrossFade(HitStunHash, crossFadeDuration);
    }

    public override void FixedUpdate()
    {
        //Stop Player Collider
    }
}