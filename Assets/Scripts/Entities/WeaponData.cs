using UnityEngine;

namespace Entities
{
    [CreateAssetMenu(fileName = "MeleeWeapon", menuName = "Weapon")]
    public class WeaponData : EntityData
    {
        
        [SerializeField]private new string _name;
    
        [SerializeField]private float _attackDuration;
        [SerializeField]private float _attackCooldown;
    
        [SerializeField]private int _damage;
        
        private protected static readonly int RunHash = Animator.StringToHash("Run");

        public string name=>_name;
    
        public float attackDuration=>_attackDuration;
        public float attackCooldown=>_attackCooldown;
    
        public int damage=>_damage;
        
        public int animationHash => RunHash;
    }
}