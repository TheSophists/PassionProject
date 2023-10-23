using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    Inventory inventory;

    public GameObject UI;           //Canvas used for the player UI, named "Inventory Canvas" in the inspector, under Player->Main Camera

    public Transform itemsParent;   //empty Game object that is the Parent Object for every inventory slot

    InventorySlot[] slots;          //array of Game objects with the component "InventorySlot" script.

    bool flag;                      //used to flip the inventory UI on/off.


    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;     //get the instance of the inventory

        inventory.onItemChangedCallback += UpdateUI;    //add an event for updating the UI with a new inventory item

        UI.transform.GetChild(0).gameObject.SetActive(false);   //keep the inventory UI turned off at the start.
        flag = false;
    }

    // Update is called once per frame
    void Update()
    {
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();       //the slots array gets every object with an inventory slot component that is stored as a child of the "Inventory gameobject"
                                                                            //this is all found under the player Game object.

        if (Input.GetKeyDown(KeyCode.I))                        //if the player presses the "I" key
        {
            flag = !flag;                                           //flip the flag.
            UI.transform.GetChild(0).gameObject.SetActive(flag);    //set the UI Active if the flag is true, deactivate if the flag is false.
        }
    }

    void UpdateUI()
    {
        for(int i = 0; i < slots.Length; i++)       //for each inventory SLOT that the player has access to. slots.Length is set manually, and some slots can be empty.
        {
            //if the inventory slot iterator is less than the number of items in our inventory. (inventory is not full)
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);   //this item is supposed to be in our inventory, so add it to its new slot (bassed on its location in the inventory.items aray)
            }

            //if the inventory slot iterator is higher than the number of items in our inventory.
            else
            {
                slots[i].ClearSlot();   //we are displaying an item that we no longer have, so remove 
            }
        }
    }
}
