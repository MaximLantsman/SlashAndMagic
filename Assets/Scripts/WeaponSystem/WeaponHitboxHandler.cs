using System.Collections;
using System.Collections.Generic;
using Damagable;
using UnityEngine;

public class WeaponHitboxHandler : MonoBehaviour
{
    
    private int enemyLayer;
    private int currentDamage;

    private Collider weaponHitbox;
    private List<GameObject> hittedEnemies = new List<GameObject>();

    private void Awake()
    {
        enemyLayer = LayerMask.NameToLayer("Enemy");
        weaponHitbox = GetComponent<Collider>();
    } 
    
    public void PreformHitboxAttack(float attackDuration,int attackDamage)
    {
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
        if (!hittedEnemies.Contains(other.gameObject) && other.TryGetComponent(out IDamagable damaged))
        {
            damaged.Damage(currentDamage);
            hittedEnemies.Add(other.gameObject);
        }
    }
}