using StateMechine;
using UnityEngine;

public class DashState : BaseState
{
    public DashState(PlayerController player, Animator animator) : base(player, animator) { }

    public override void OnEnter()
    {
        Debug.Log("Dash start");
        animator.CrossFade(IdleHash, crossFadeDuration);
    }

    public override void FixedUpdate()
    {
        Debug.Log("Dashing");
        player.HandleMovement();
        player.HandleDash();
    }
}
