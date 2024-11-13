using StateMechine;
using UnityEngine;

public class LocomotionState : BaseState
{
    public LocomotionState(PlayerController player, Animator animator): base(player, animator) {}

    public override void OnEnter()
    {
        Debug.Log("move start");
        animator.CrossFade(IdleHash, crossFadeDuration);
    }

    public override void FixedUpdate()
    {
        Debug.Log("moving");
        player.HandleMovement();
    }
}
