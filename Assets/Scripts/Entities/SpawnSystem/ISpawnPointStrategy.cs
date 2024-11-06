using UnityEngine;

namespace Entities.SpawnSystem
{
    public interface ISpawnPointStrategy
    {
        Transform NextSpawnPoint();
    }
}
