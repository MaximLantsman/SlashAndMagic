using StateMechine;
using UnityEngine;

public class AttackState : BaseState
{
    private int weaponAnimation = Animator.StringToHash("AttackSword");
    public AttackState(PlayerController player, Animator animator): base(player, animator) {}

    public override void OnEnter()
    {
        Debug.Log("Enter Attack");
        

        animator.CrossFade(weaponAnimation, crossFadeDuration);
    }

    public override void SwitchAttackAnim(int newAttackHash)
    {
        weaponAnimation = newAttackHash;
    }

    public override void FixedUpdate()
    {
        Debug.Log("Attacking");
        player.HandleAttack();
    }
}