using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public delegate void onItemChanged();       //delegate used to signal when the inventory item list has changed. used in inventory UI and this script
    public onItemChanged onItemChangedCallback;

    public List<InventoryItemData> items = new List<InventoryItemData>();       //list of data that represents the inventory items
    private Dictionary<InventoryItemData, InventoryItem> m_itemDictionary;      //dictionary that links the inventory data to the actual inventory item

    public static Inventory instance;       //instance of the inventory

    public int space = 28;              //number of items that can be held in the inventory

    private void Awake()
    {
        m_itemDictionary = new Dictionary<InventoryItemData, InventoryItem>();      //create a new dictionary to hold data/items

        if (instance != null)       //if we have an active inventory
        {
            Debug.LogWarning("More than one instance of inventory found");  //we shouldnt have an inventory on Awake() yet, so give a warning
        }
        instance = this;       //else, create the instance.

    }

    public InventoryItem Get(InventoryItemData referenceData)                       //gets info about the item in our inventory
    {
        if (m_itemDictionary.TryGetValue(referenceData, out InventoryItem value))   //check the dictionary for any value matching the reference data being sent.
        {                                                                           //if we have that data in the inventory, pull the Inventory Item associated with it out and store it in "value"
            return value;
        }
        return null;                                                                //if we dont have any data in the dictionary that matches our reference data, we dont have the item, so return null
    }

    public bool Add(InventoryItemData item)                                         //add an item to the inventory, using its inventory item data values, called from "item pickup" script
    {

        if (m_itemDictionary.TryGetValue(item, out InventoryItem value))        //if this item data already exists in our dictionary of items
        {
            value.AddToStack();                                                 //simply add one to the stack size.
        }
        else if (items.Count >= space)                                          //else if our inventory is already full
        {
            Debug.Log("Not enough room");       //warn the player and return false, as we werent successful in adding the item
            return false;                       //this return statement avoids the item changed callback below, as this is the only option that doesnt need to update the UI
        }
        else                                    //otherwise it is safe to add the item to the inventory.
        {
            InventoryItem newItem = new InventoryItem(item);    //create a new inventory item based on the reference data "item"
            items.Add(item);                                    //add the new "Inventory Item" to the inventory. (should be things like the icon displayed in the UI)
            m_itemDictionary.Add(item, newItem);                //add both the data and the item to our dictionary

        }
        if (onItemChangedCallback != null)      //if we have an onItemChangedCallback ?delegate?
        {
            onItemChangedCallback.Invoke();     //invoke it, as we have updated the inventory.
        }

        return true;    //return true if we successfully added an item. (should be avoided if the if/else above is executed)
    }

    public void Remove(InventoryItemData item)      //removes an item from the dictionary
    {
        m_itemDictionary.TryGetValue(item, out InventoryItem value);        //check if this inventoryItemData exists in our dictionary already.

        if (value.stackSize == 1)       //if there is only 1 of these objects in our inventory.
        {
            items.Remove(item);         //remove the item from the list of items and our dictionary
            m_itemDictionary.Remove(item);
        }
        else                //otherwise just remove 1 from its stack size
        {
            value.RemoveFromStack();
        }


        if (onItemChangedCallback != null)      //if either of thesse if statements are executed, we will need to update the UI with those changes, so invoke the delegate.
        {
            onItemChangedCallback.Invoke();
        }
    }

}
