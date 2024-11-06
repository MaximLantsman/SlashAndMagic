using UnityEngine;

namespace Entities.SpawnSystem
{
    public interface IEntityFactory<T> where T : Entity
    {
        T Create(Transform spawnPoint);
    }
}
