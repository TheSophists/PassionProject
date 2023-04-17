using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public InventoryItemData item;      //the item that will be added to the inventory (set in the inspector)
    bool flag = true;                   //flag ussed to make sure a collision doesnt happen twice in 1 frame.


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && flag == true)     //if the Player has collided with this object.
        {
            Inventory.instance.Add(item);                       //add this item to their inventory.
            Destroy(this.gameObject);                           //destroy the world object.

            flag = false;                                       //reset the flag.
        }
    }

    private void OnTriggerExit2D(Collider2D collision)          //if the player leaves the collider, reset the flag.
    {
        flag = true;                                            //afaik, this code shouldnt execute with the current build.
    }
}
