using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetDungeon : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(DungeonManager.instance.newDungeon == false && collision.tag == "Player")
        {
            DungeonManager.instance.ResetDungeon();
        }
    }
}
