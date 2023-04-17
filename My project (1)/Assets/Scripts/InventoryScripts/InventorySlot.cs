using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class InventorySlot : MonoBehaviour
{
    public Image icon;              //location for the icon for the "use button" in the inventory UI, not the actual sprite
    public Button removeButton;     //button pressed to remove an item from the inventory slot
    public Text itemName;           //text box for name
    public Text stack;              //text box for stack size

    public GameObject player;
    public InventoryItem inventItem;

    InventoryItemData item;


    public void AddItem(InventoryItemData newItem)      //adds an item data to the inventory
    {
        item = newItem; //the item that is to be added

        inventItem = Inventory.instance.Get(item);      //instance of the inventory
        stack.text = inventItem.stackSize.ToString();   //stack size being caste to a string then stored in the text box for display


        itemName.text = item.displayName;               //item name being added to a text box
        icon.sprite = item.icon;                        //add item sprite to the proper location
        icon.enabled = true;                            //turn the icon on (disabled until something fills the slot)
        removeButton.interactable = true;               //turn on the remove button (disabled until filled)

    }

    public void ClearSlot()     //similar to AddItem. This simnply resets all of the data to null/inactive when an item is removed from the inventory.
    {
        item = null;

        stack.text = null;
        itemName.text = null;
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    public void OnRemoveButton()                //when the remove button is pressed. the player should drop the item on the ground.
    {
        inventItem = Inventory.instance.Get(item);  //get the item in the instance of the inventory.
        if (player == null)                         //if we dont have the player's game object yet.
        {
            player = GameObject.Find("Player");     //find it
        }

        //then spawn the gameobject just in front of the player.
        Vector3 offset = new Vector3(Random.Range(4f, 6f), 0, 0);
        Instantiate(item.prefab, (player.transform.position + offset), Quaternion.identity);

        //remove that item from the inventory. (not just the slot)
        Inventory.instance.Remove(item);
    }

    //this may be redundant/unnecessary
    public void UseItem()   //this is attached to the button that is placed on top of the of the inventory icon, so when the player clicks the icon, it uses the item.
    {
        if (item != null)   //if the item exists.
        {
            item.Use();     //use it
        }
    }
}