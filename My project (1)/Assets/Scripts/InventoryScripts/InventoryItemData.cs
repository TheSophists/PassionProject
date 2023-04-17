using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "My Game/Inventory Item Data/InventoryItemData")]    //creates a menu option for creating a scriptable object (inventory item data)
public class InventoryItemData : ScriptableObject
{
    public string id = "New Item";
    public string displayName;
    public Sprite icon = null;      //inventory sprite.
    public GameObject prefab;       //in game object

    public virtual void Use()       //code that will be executed when the player clicks this item in their inventory.
    {
        //this is intended to be overwritten by specific functions that result in different thing. (health potions heal, equipment gets equipped)

        Debug.Log("Using " + name);
    }

    public void RemoveFromInventory()   //code that executes when a player tries to drop an item from their inventory.
    {
        Inventory.instance.Remove(this);//simply removes the item from the inventory.
    }
}
