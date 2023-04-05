using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public static DungeonManager instance;  //creates an instance of the DungeonManager
    public GameObject dungeon;

    public Transform parent;            //parent of the dungeon (currently the Enemies empty object)

    public bool newDungeon = false;     //flag to see if we have a a fresh dungeon or not. this flips to false after the boss dies (look at Boss Stats script)
    GameObject oldDungeon;              //stores the old, completed dungeon


    private void Awake()    
    {
        instance = this;        //set the singleton
        oldDungeon = Instantiate(dungeon, parent);  //instantiates a fresh, incomplete dungeon. Instantiates doors and enemies.
        instance.newDungeon = true;                 //because we just instantiated a new dungeon, we set this as new.
    }

    public void ResetDungeon()
    {
        Destroy(oldDungeon);                            //once the dungeon is complete, remove all of the old enemies/doors.
        oldDungeon = Instantiate(dungeon, parent);      //re-instantiate the original, incomplete dungeon. 
        instance.newDungeon = true;                     //this results in a "new dungeon"
    }
}
