using System.Collections.Generic;
using UnityEngine;

namespace Entities.SpawnSystem
{
    public class EnemySpawnManager : EntitySpawnerManager
    {
        [SerializeField] private int budget = 3;
        
        [SerializeField]private PlayerController player;

        
        [SerializeField]private EnemyData[] enemyData;
        [SerializeField]private float spawnInterval = 1f;
        
        private EntitySpawner<Enemy> spawner;

        public static Dictionary<Collider,Health.Health> activeEnteties = new Dictionary<Collider,Health.Health>();
        
        
        private protected override void Awake()
        {
            base.Awake();
            activeEnteties.Add(player.collider,player.health);
            EnemyCreation();
        }

        private void EnemyCreation()
        {
            for (int i = 0; i < enemyData.Length; i++)
            {
                spawner = new EntitySpawner<Enemy>(new EntityFactory<Enemy>(new EnemyData[] { enemyData[i]}), spawnPointStrategy);
                Enemy enemyCreatedInstance = spawner.Spawn();
                enemyCreatedInstance.player = player.gameObject.transform;
                enemyCreatedInstance.OnInitialized();
                
                activeEnteties.Add(enemyCreatedInstance.enemyCollider, enemyCreatedInstance.health);
            }
        }

        public override void Spawn()
        {
            
        }
    }
}


