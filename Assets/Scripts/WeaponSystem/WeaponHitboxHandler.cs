using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Entities.SpawnSystem.EnemySpawnManager;

public class WeaponHitboxHandler : MonoBehaviour
{
    
    private int currentDamage;
    private PlayerController player;
    private Collider weaponHitbox;
    private List<GameObject> hittedEnemies = new List<GameObject>();


    public void PreformHitboxAttack(float attackDuration,int attackDamage,Collider weaponCollider)
    {
        weaponHitbox = weaponCollider;
        weaponHitbox.enabled = true;
        currentDamage = attackDamage;

        StartCoroutine(StopHitboxAttack(attackDuration));
    }

    private IEnumerator StopHitboxAttack(float duration)
    {
        yield return new WaitForSeconds(duration);
        weaponHitbox.enabled = false;
        hittedEnemies.Clear();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!hittedEnemies.Contains(other.gameObject) && activeEnteties.TryGetValue(other, out var takeDamage))
        {
            takeDamage.TakeDamage(currentDamage);
            hittedEnemies.Add(other.gameObject);
        }
    }
}