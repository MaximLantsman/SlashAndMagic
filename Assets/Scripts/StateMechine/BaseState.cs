﻿using UnityEngine;

namespace StateMechine
{
    public abstract class BaseState : IState
    {
        private protected readonly PlayerController player;
        private protected readonly Animator animator;
        
        private protected static readonly int IdleHash = Animator.StringToHash("Idle");
        private protected static readonly int DashHash = Animator.StringToHash("Dash");
        private protected static readonly int RunHash = Animator.StringToHash("Run");
        private protected static readonly int HitStunHash = Animator.StringToHash("HitStun");
        private protected static readonly int DeathHash = Animator.StringToHash("Death");
        
        private protected static readonly int BowIdleHash = Animator.StringToHash("Idle");
        private protected static readonly int BowRunHash = Animator.StringToHash("Run");
        private protected static readonly int BowHitStunHash = Animator.StringToHash("HitStun");
        private protected static readonly int BowDeathHash = Animator.StringToHash("Death");
        //private protected static readonly int AttackHash = Animator.StringToHash("Attack");
        
        protected const float crossFadeDuration = 0.1f;

        protected BaseState(PlayerController player, Animator animator)
        {
            this.player = player;
            this.animator = animator;
        }
        
        public virtual void OnEnter()
        {
            //noop
        }

        public virtual void Update()
        {
            //noop

        }
        

        public virtual void FixedUpdate()
        {
            //noop

        }

        public virtual void SwitchAttackAnim(int newAttackHash)
        {
        }

        public virtual void OnExit()
        {
            //noop
        }
    }
}