using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDoor : MonoBehaviour
{
    public GameObject door;     //the door object that will be closed
    public GameObject enemies;  //the enemies that are in the room the player just entered. (enemies spawn after the player walks through the door location that is about to be closed.

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")  //if the player walks through the door.
        {
            door.SetActive(true);   //close the door behind the player.
            if (enemies != null)    //if the room has enemies associated with it
            {
                enemies.SetActive(true);    //spawn them after the player enters the room and the door shuts behind.
            }
        }

    }
}
