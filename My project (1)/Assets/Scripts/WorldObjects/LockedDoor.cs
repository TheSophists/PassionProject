using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    bool playerInside = false;
    Inventory inventory;
    public InventoryItemData itemData;

    private void Start()
    {
        inventory = Inventory.instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerInside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerInside = false;
        }
    }

    private void Update()
    {
        if(playerInside == true && Input.GetButtonDown("Interact"))
        {
            if (inventory.Get(itemData) != null)
            {
                inventory.Remove(itemData);
                this.gameObject.SetActive(false);
            }
        }
    }
}
