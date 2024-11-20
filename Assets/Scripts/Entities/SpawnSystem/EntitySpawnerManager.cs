using UnityEngine;

namespace Entities.SpawnSystem
{
    public abstract class EntitySpawnerManager : MonoBehaviour
    {
        [SerializeField]private protected SpawnPointStrategyType spawnPointStrategyType = SpawnPointStrategyType.Linear;
        [SerializeField]private protected Transform[] spawnPoints;

        private protected ISpawnPointStrategy spawnPointStrategy;

        private protected enum SpawnPointStrategyType
        {
            Linear,
            Random
        }

        private protected virtual void Awake()
        {
            spawnPointStrategy = spawnPointStrategyType switch
            {
                SpawnPointStrategyType.Linear => new LinearSpawnPointStrategy(spawnPoints),
                SpawnPointStrategyType.Random => new RandomSpawnPointStrategy(spawnPoints),
                _ => spawnPointStrategy
            };
        }

        public abstract void Spawn();
    }
}