using StateMechine;
using UnityEngine;

public class DashState : BaseState
{
    public DashState(PlayerController player, Animator animator) : base(player, animator) { }

    public override void OnEnter()
    {
        animator.CrossFade(IdleHash, crossFadeDuration);
    }

    public override void FixedUpdate()
    {
        player.HandleMovement();
        player.HandleDash();
    }
}