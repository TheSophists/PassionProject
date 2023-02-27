using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class InventoryItem
{
    public int stackSize { get; private set; }
    public InventoryItemData data { get; private set; }


    public InventoryItem(InventoryItemData source)
    {
        data = source;
        AddToStack();
    }
    public void AddToStack()
    {
        stackSize++;
    }

    public void RemoveFromStack()
    {
        stackSize--;
    }
}
