using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class LootChest : MonoBehaviour
{
    bool playerInside = false;
    bool flag = true;
    public DropChestItem dropItem;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")      //if something collides with the npc, check to see if its the player.
        {
            playerInside = true;            //if it is, set this boolean to true, so that if the player presses the interact key while next to the npc, it starts dialog.
        }
    }

    public void Update()
    {
        if (Input.GetButtonDown("Interact") && playerInside == true && flag == true)        //if the player wants to interact, is inside the npc's trigger, and the interact button hasnt already been pressed.
        {
            flag = false;                   //flip the input check flag
            dropItem.PickItem();
        }
    }
}
