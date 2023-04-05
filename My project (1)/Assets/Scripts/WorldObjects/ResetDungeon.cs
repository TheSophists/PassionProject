using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetDungeon : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)  
    {
        if(DungeonManager.instance.newDungeon == false && collision.tag == "Player")    //when the player triggers the dungeon exit trigger (after defeating the boss, its similar to a door.) and we have an old dungeon
        {
            DungeonManager.instance.ResetDungeon();     //reset the dungeon (simply removes the old object and reinstates its original prefab so the enemies/doors are replaced properly.
        }
    }
}
