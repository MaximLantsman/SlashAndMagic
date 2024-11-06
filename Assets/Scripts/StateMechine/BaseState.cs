using UnityEngine;

namespace StateMechine
{
    public abstract class BaseState : IState
    {
        private protected readonly PlayerController player;
        private protected readonly Animator animator;
        
        private protected static readonly int IdleHash = Animator.StringToHash("Idle");
        private protected static readonly int RunHash = Animator.StringToHash("Run");

        private protected const float crossFadeDuration = 0.1f;

        private protected BaseState()
        {
            this.player = player;
            this.animator = animator;
        }
        
        public virtual void OnEnter()
        {
            throw new System.NotImplementedException();
        }

        public virtual void Update()
        {
            throw new System.NotImplementedException();
        }

        public virtual void FixedUpdate()
        {
            throw new System.NotImplementedException();
        }

        public virtual void OnExit()
        {
            throw new System.NotImplementedException();
        }
    }
}