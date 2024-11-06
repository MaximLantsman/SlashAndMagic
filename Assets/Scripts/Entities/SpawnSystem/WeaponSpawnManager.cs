using UnityEngine;

namespace Entities.SpawnSystem
{
    public class WeaponSpawnManager : EntitySpawnerManager
    {
        [SerializeField]private WeaponData[] weaponData;
        [SerializeField]private float spawnInterval = 1f;
        
        private EntitySpawner<Weapon> spawner;
        
        private protected override void Awake()
        {
            base.Awake();
            for (int i = 0; i < weaponData.Length; i++)
            {
                spawner = new EntitySpawner<Weapon>(new EntityFactory<Weapon>(new WeaponData[] { weaponData[i]}), spawnPointStrategy);
                Spawn();
            }
            
        }
        
        public override void Spawn() => spawner.Spawn();
    }
}