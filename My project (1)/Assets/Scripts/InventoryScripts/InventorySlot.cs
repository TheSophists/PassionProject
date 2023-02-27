using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Button removeButton;
    public Text itemName;
    public Text stack;

    public GameObject player;
    public InventoryItem inventItem;

    InventoryItemData item;


    public void AddItem(InventoryItemData newItem)
    {
        item = newItem;

        inventItem = Inventory.instance.Get(item);
        stack.text = inventItem.stackSize.ToString();


        itemName.text = item.displayName;
        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;

    }

    public void ClearSlot()
    {
        item = null;

        stack.text = null;
        itemName.text = null;
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    public void OnRemoveButton()
    {
        inventItem = Inventory.instance.Get(item);
        if (player == null)
        {
            player = GameObject.Find("Player");
        }


        Vector3 offset = new Vector3(Random.Range(4f, 6f), 0, 0);
        Instantiate(item.prefab, (player.transform.position + offset), Quaternion.identity);
        Inventory.instance.Remove(item);
    }

    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
        }
    }
}