using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Entities.SpawnSystem
{
    public class RandomSpawnPointStrategy : ISpawnPointStrategy
    {
        List<Transform> unusedSpawnPoints;
        Transform[] spawnPoints;

        public RandomSpawnPointStrategy(Transform[] spawnPoints)
        {
            this.spawnPoints = spawnPoints;
            unusedSpawnPoints = new List<Transform>(spawnPoints);
        }

        public Transform NextSpawnPoint()
        {
            if (!unusedSpawnPoints.Any())
            {
                unusedSpawnPoints = new List<Transform>(spawnPoints);
            }
            int randomIndex = Random.Range(0, unusedSpawnPoints.Count);
            Transform result = unusedSpawnPoints[randomIndex];
            unusedSpawnPoints.RemoveAt(randomIndex);
            return result;
        }
    }
}