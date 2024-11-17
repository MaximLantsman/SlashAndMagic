using StateMechine;
using UnityEngine;

public class LocomotionState : BaseState
{
    public LocomotionState(PlayerController player, Animator animator): base(player, animator) {}

    public override void OnEnter()
    {
        animator.CrossFade(IdleHash, crossFadeDuration);
    }

    public override void FixedUpdate()
    {
        player.HandleMovement();
    }
}
