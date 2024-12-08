using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class ProjectileHitBoxHandler : MonoBehaviour
{
    [SerializeField] private ProjectileMovement projectile;
    [SerializeField] private Collider weaponHitbox;
    
    private UniTask weaponHitTask;
    private int currentDamage;
    private PlayerController player;
    
 
    
    private List<GameObject> hittedEnemies = new List<GameObject>();


    public async void PreformHitboxAttack(float attackDuration,int attackDamage,Collider weaponCollider)
    {
        //projectile.
        projectile.enabled = true;
        weaponHitbox.enabled = true;
        currentDamage = attackDamage;
        //add start collider delay for attack wind up - another unitask
        
        //StartCoroutine(StopHitboxAttack(attackDuration));
        CancellationTokenSource cts = new CancellationTokenSource();
        CancellationToken token = cts.Token;
        
        await StopHitBoxAttack(attackDuration,token);
    }

    private async UniTask StopHitBoxAttack(float duration,CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        await UniTask.Delay(TimeSpan.FromSeconds(duration), cancellationToken: token);
        
        //Do same with weapon? i think yes!
        projectile.enabled = false;
        weaponHitbox.enabled = true;
        projectile.hittedEnemies.Clear();
    }
    
}
