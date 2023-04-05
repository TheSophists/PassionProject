using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    bool playerInside = false;          //is the player inside the collider that checks to see if the player can unlock the door.
    Inventory inventory;                //instance of the player's inventory.
    public InventoryItemData key;  //set in the inspector, the specific item that is used to open the door

    private void Start()
    {
        inventory = Inventory.instance;     //get the player's current inventory.
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")      //if a player enters the trigger of a locked door
        {
            playerInside = true;            //then playerInside becomes true (crazy, right?)
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")      //if the player exits the trigger associated with the door
        {
            playerInside = false;           //then the player is no longer inside. (absolutely incredible)
        }
    }

    private void Update()
    {
        if(playerInside == true && Input.GetButtonDown("Interact"))     //if the player is inside the locked door check, and they interact with the door
        {
            if (inventory.Get(key) != null)    //and if the inventory contains the item data associated with opening the door
            {
                inventory.Remove(key);              //remove the key from the inventory
                this.gameObject.SetActive(false);   //disable the locked door object that this script is currently attached to.
            }
        }
    }
}
