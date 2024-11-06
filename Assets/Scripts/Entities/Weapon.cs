using Entities;
using UnityEngine;
using WeaponSystem;

namespace Entities
{
    public class Weapon : Entity, ICommandAttack
    {

        [SerializeField] private WeaponData weaponData;
        [SerializeField] private string animationName;
        
        public WeaponHitboxHandler weaponInstance;
        
        
        public WeaponData _weaponData => weaponData;
        public string _animationName => animationName;
        public GameObject _weaponPrefab => weaponData.prefab;


        public void Attack()
        {
            if (weaponInstance!=null )
            {
                weaponInstance.PreformHitboxAttack(weaponData.attackDuration,weaponData.damage);
            }
        }
        
    }
}    
