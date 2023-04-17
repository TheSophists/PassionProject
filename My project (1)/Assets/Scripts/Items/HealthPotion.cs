using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthPotion", menuName = "My Game/Inventory Item Data/Health Potion")]
public class HealthPotion : InventoryItemData
{
    PlayerManager playerManager;
    PlayerStats playerStats;
    public int heal;            //the amount that this potion will heal

    void GetData()      //get info about the player such as current health (STATS).
    {
        playerManager = PlayerManager.instance;     //get the instance of the player
        playerStats = playerManager.player.GetComponent<PlayerStats>();     //get the player stats component

    }
    public override void Use()
    {
        base.Use();     //execute the base class for Use()
        GetData();      //get the player's data

        playerStats.Heal(heal);     //heal the player, based on how much this item heals for.
        RemoveFromInventory();      //remove it from the inventory, as it has been consumed.
    }
}
