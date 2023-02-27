using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "My Game/Inventory Item Data/InventoryItemData")]
public class InventoryItemData : ScriptableObject
{
    public string id = "New Item";
    public string displayName;
    public Sprite icon = null;
    public GameObject prefab;

    public virtual void Use()
    {
        //use items, something happens

        Debug.Log("Using " + name);
    }

    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }
}
