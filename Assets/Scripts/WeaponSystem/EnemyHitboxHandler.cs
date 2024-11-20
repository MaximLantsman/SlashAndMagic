using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitboxHandler : MonoBehaviour
{
    private int currentDamage;

    private Collider weaponHitbox;
    private List<GameObject> hittedEnemies = new List<GameObject>();
    
    private PlayerController player;


    public void PreformHitboxAttack(float attackDuration,int attackDamage,Collider weaponCollider, PlayerController playerSent)
    {
        weaponHitbox = weaponCollider;
        weaponHitbox.enabled = true;
        currentDamage = attackDamage;
        player = playerSent;
        
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
        //if (other.gameObject == player.gameObject || activeEnemies[other] != null)
        //{
            
        //}
    }
}