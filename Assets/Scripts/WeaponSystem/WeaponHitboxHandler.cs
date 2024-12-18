using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

using static Entities.SpawnSystem.EnemySpawnManager;

public class WeaponHitboxHandler : MonoBehaviour
{
    private UniTask weaponHitTask;
    private int currentDamage;
    private PlayerController player;
    private Collider weaponHitbox;
    private List<GameObject> hittedEnemies = new List<GameObject>();


    public async void PreformHitboxAttack(float attackDuration,int attackDamage,Collider weaponCollider)
    {
        weaponHitbox = weaponCollider;
        weaponHitbox.enabled = true;
        currentDamage = attackDamage;

        //add start collider delay for attack wind up - another unitask
        
        //StartCoroutine(StopHitboxAttack(attackDuration));
        CancellationTokenSource cts = new CancellationTokenSource();
        CancellationToken token = cts.Token;
        
        await  StopHitBoxAttack(attackDuration,token);
    }

    private async UniTask StopHitBoxAttack(float duration,CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        await UniTask.Delay(TimeSpan.FromSeconds(duration), cancellationToken: token);

        weaponHitbox.enabled = false;
        hittedEnemies.Clear();
    }


    /*private IEnumerator StopHitboxAttack(float duration)
    {
        yield return new WaitForSeconds(duration);
        weaponHitbox.enabled = false;
        hittedEnemies.Clear();
    }*/
    
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (!hittedEnemies.Contains(other.gameObject) && activeEnteties.TryGetValue(other, out var takeDamage))
        {
            takeDamage.TakeDamage(currentDamage);
            hittedEnemies.Add(other.gameObject);
        }
    }
}