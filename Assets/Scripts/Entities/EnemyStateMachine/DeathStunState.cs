using StateMechine;
using UnityEngine;

public class DeathStunState : BaseState
{
    public DeathStunState(PlayerController player, Animator animator) : base(player, animator) { }

    public override void OnEnter()
    {
        Debug.Log("Death Stun");
        animator.CrossFade(DeathHash, crossFadeDuration);
    }

  
}