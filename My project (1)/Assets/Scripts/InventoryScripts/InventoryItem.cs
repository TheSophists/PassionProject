using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class InventoryItem
{
    public int stackSize { get; private set; }      //this variable can be read, but not written to outside this class
    public InventoryItemData data { get; private set; } 


    public InventoryItem(InventoryItemData source)  //creates a new item in the inventory.
    {
        data = source;      //set the data associated with this inventory item
        AddToStack();       //adds 1 to the stack size
    }

    public void AddToStack()    //adds 1 to the stack size.
    {
        stackSize++;
    }

    public void RemoveFromStack()   //removes 1 from the stack size
    {
        stackSize--;
    }
}
