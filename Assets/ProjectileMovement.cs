using System.Collections.Generic;
using UnityEngine;

using static Entities.SpawnSystem.EnemySpawnManager;


public class ProjectileMovement : MonoBehaviour
{
    [SerializeField] private float projectileVelocity;

    public List<GameObject> hittedEnemies = new List<GameObject>();

    
    // Update is called once per frame
    private void Update()
    {
        transform.Translate(Vector3.forward * projectileVelocity * Time.deltaTime);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!hittedEnemies.Contains(other.gameObject) && activeEnteties.TryGetValue(other, out var takeDamage))
        {
            //takeDamage.TakeDamage(currentDamage);
            hittedEnemies.Add(other.gameObject);
        }
    }
}
