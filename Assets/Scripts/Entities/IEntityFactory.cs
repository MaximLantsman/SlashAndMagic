using UnityEngine;

namespace Entities
{
    public interface IEntityFactory<T> where T: Entity
    {
        T Create(Transform spawnPoint);
    }
}

