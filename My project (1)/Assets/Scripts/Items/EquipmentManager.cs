using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager instance;

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;


    Inventory inventory;

    private void Awake()
    {
        {
            instance = this;        //singleton that creates the equipment
        }
    }


    Equipment[] currentEquipment;
    private void Start()
    {
        inventory = Inventory.instance;
        {
            int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
            currentEquipment = new Equipment[numSlots];
        }
    }

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.equipSlot;

        Equipment oldItem = null;

        InventoryItem data = inventory.Get(newItem);
        newItem.itemAmount = data.stackSize;


        if (currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            for (int i = 0; i < oldItem.itemAmount; i++)
            {
                inventory.Add(oldItem);
            }
        }

        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }

        currentEquipment[slotIndex] = newItem;
    }

    public void Unequip(int slotIndex)
    {
        if (currentEquipment[slotIndex] != null)
        {
            Equipment oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);

            currentEquipment[slotIndex] = null;

            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldItem);
            }
        }
    }

    public void UnequipAll()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            UnequipAll();
        }
    }

    public Equipment GetBuilds()
    {
        return currentEquipment[4];
    }

    public Equipment GetRanged()
    {
        return currentEquipment[1];
    }

    public Equipment GetMelee()
    {
        return currentEquipment[2];
    }
}
