using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthPotion", menuName = "My Game/Inventory Item Data/Health Potion")]
public class HealthPotion : InventoryItemData
{
    PlayerManager playerManager;
    PlayerStats playerStats;
    public int heal;

    void GetData()
    {
        playerManager = PlayerManager.instance;
        playerStats = playerManager.player.GetComponent<PlayerStats>();
        Debug.Log(playerStats.baseHealth.GetValue());

    }
    public override void Use()
    {
        base.Use();
        GetData();
        playerStats.Heal(heal);
        RemoveFromInventory();
    }
}
