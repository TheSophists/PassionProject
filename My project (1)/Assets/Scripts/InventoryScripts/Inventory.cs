using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public delegate void onItemChanged();
    public onItemChanged onItemChangedCallback;

    public List<InventoryItemData> items = new List<InventoryItemData>();
    private Dictionary<InventoryItemData, InventoryItem> m_itemDictionary;

    public static Inventory instance;

    public int space = 28;

    private void Awake()
    {
        m_itemDictionary = new Dictionary<InventoryItemData, InventoryItem>();

        if (instance != null)
        {
            Debug.LogWarning("More than one instance of inventory found");
        }
        instance = this;
    }

    public InventoryItem Get(InventoryItemData referenceData)
    {
        if (m_itemDictionary.TryGetValue(referenceData, out InventoryItem value))
        {
            return value;
        }
        return null;
    }

    public bool Add(InventoryItemData item)
    {

        if (m_itemDictionary.TryGetValue(item, out InventoryItem value))
        {
            value.AddToStack();
        }
        else if (items.Count >= space)
        {
            Debug.Log("Not enough room");
            return false;
        }
        else
        {
            InventoryItem newItem = new InventoryItem(item);
            items.Add(item);
            m_itemDictionary.Add(item, newItem);

        }

        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }

        return true;
    }

    public void Remove(InventoryItemData item)
    {
        m_itemDictionary.TryGetValue(item, out InventoryItem value);


        if (value.stackSize == 1)
        {
            items.Remove(item);
            m_itemDictionary.Remove(item);
        }
        else
        {
            value.RemoveFromStack();
        }


        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }

}
