using System;
using Entities;
using KBCore.Refs;
using UnityEngine;
using WeaponSystem;

namespace Entities
{
    public class Weapon : Entity, ICommandAttack
    {

        [SerializeField] private WeaponData weaponData;
        [SerializeField] private string animationName;

        public Collider weaponCollider;
        
        private WeaponHitboxHandler weaponInstance;
        
        public WeaponData _weaponData => weaponData;
        public string _animationName => animationName;
        public GameObject _weaponPrefab => weaponData.prefab;

        private void Awake()
        {
            weaponInstance = GetComponent<WeaponHitboxHandler>();
        }

        public void Attack()
        {
            if (weaponInstance!=null )
            {
                weaponInstance.PreformHitboxAttack(weaponData.attackDuration, weaponData.damage, weaponCollider);
            }
        }
        
    }
}    
