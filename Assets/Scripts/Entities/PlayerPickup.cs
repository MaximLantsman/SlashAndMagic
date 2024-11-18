using Entities;
using KBCore.Refs;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    
    private readonly string playerTag = "Player";
    private  int playerLayer;

    private void Awake()
    {
        playerLayer = LayerMask.NameToLayer(playerTag);
    }
    
    /*private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.layer == playerLayer)
        {
            //playerController.WeaponEquip(weapon);
        }
    }*/
}
