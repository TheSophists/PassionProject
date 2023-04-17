using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager instance;

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;


    Inventory inventory;                //current inventory (after we set it equal to the Inventory instance.
    Equipment[] currentEquipment;       //array that stores the current equipment

    private void Awake()
    {
        {
            instance = this;            //singleton that manages the equipment that is currently equipped
        }
    }

    private void Start()
    {
        inventory = Inventory.instance; //get our current inventory
        {
            int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;  //get the length of the equipment array
            currentEquipment = new Equipment[numSlots];                         //create a new array that has the proper number of slots for our equipment types
        }
    }

    public void Equip(Equipment newItem)            //handles equipping a new item.
    {
        int slotIndex = (int)newItem.equipSlot;     //set the slot index to the equip slot value associated with this Equipment Object. set in "Equipment"

        Equipment oldItem = null;                   //reset the data associated with the item we are unequipping (reset because its called multiple times with different slots)

        InventoryItem data = inventory.Get(newItem);        //get the inventory data associated with what we are equipping.
        newItem.itemAmount = data.stackSize;                //get its stack size as well.


        if (currentEquipment[slotIndex] != null)            //if there is something currently in this item's slot. (wearing a helmet while trying to equip a new helmet)
        {
            oldItem = currentEquipment[slotIndex];          //the old item is equal to whatever item is currently in that slot.
            for (int i = 0; i < oldItem.itemAmount; i++)    //for each item in the stack size(itemAmount), add 1 of these items to the player's inventory. (if we have multple copies of an item
            {                                               //make sure we have the amount in the inventory after we unequip. currently equip all copies of an object, but only stats for 1 copy.
                inventory.Add(oldItem);
            }
        }

        if (onEquipmentChanged != null)                     //if we have a changed equipment event.
        {
            onEquipmentChanged.Invoke(newItem, oldItem);    //invoke that event with the new item and the old item. (this should call for the objects to be switched in the player's equipment.
        }

        currentEquipment[slotIndex] = newItem;              //add the new item to the appropriate equipment slot.
    }

    public void Unequip(int slotIndex)                      //when unequipping an item.
    {
        if (currentEquipment[slotIndex] != null)            //if there is something in that slot
        {
            Equipment oldItem = currentEquipment[slotIndex];    //the "old item" becomes what is currently equipped (and what we're trying to remove)
            inventory.Add(oldItem);                             //add that item back into the inventory.

            currentEquipment[slotIndex] = null;                 //make sure that slot is now empty.

            if (onEquipmentChanged != null)                     //if we have an equipment changed event
            {
                onEquipmentChanged.Invoke(null, oldItem);       //invoke that event with the old item, and no new item. (this doesnt handle equipping an item to a slot thats full)
            }
        }//else, do nothing, as there is no equipment there to remove.
    }

    public void UnequipAll()        //calls Unequip() on every item in the currentEquipment array.
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i);
        }
    }

    private void Update()               //checks to see if the player has pressed the "unequip all" button. currently bound to "U"
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            UnequipAll();
        }
    }


    //these are calls that can be used to find what equipment item is currently in a specific slot
    public Equipment GetArmor()
    {
        return currentEquipment[0];
    }

    public Equipment GetRanged()
    {
        return currentEquipment[1];
    }

    public Equipment GetMelee()
    {
        return currentEquipment[2];
    }

    public Equipment GetMobility()
    {
        return currentEquipment[3];
    }

    public Equipment GetBuilds()
    {
        return currentEquipment[4];
    }

    public Equipment GetAmmo()
    {
        return currentEquipment[5];
    }
}
