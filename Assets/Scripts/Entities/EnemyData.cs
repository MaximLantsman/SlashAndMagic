using Entities;
using UnityEngine;

namespace Entities
{
    [CreateAssetMenu(fileName = "Enemy", menuName = "EnemyData")]
    public class EnemyData : EntityData
    {
        [SerializeField] private new string _name;

        [SerializeField] private float _attackDuration;
        [SerializeField] private int _enemyType;

        [SerializeField] private int _damage;
        
        [SerializeField] private int _enemyManagerPrice;
        
        public string name => _name;
        public float attackDuration => _attackDuration;
        public float enemyType => _enemyType;
        public int damage => _damage;
        public int enemyManagerPrice => _enemyManagerPrice;


    }
}
