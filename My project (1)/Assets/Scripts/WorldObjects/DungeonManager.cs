using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public static DungeonManager instance;
    public GameObject dungeon;
    public Transform parent;
    public bool newDungeon = false;
    GameObject oldDungeon;
    private void Awake()
    {
        instance = this;
        oldDungeon = Instantiate(dungeon, parent);
        instance.newDungeon = true;
    }

    public void ResetDungeon()
    {
        Destroy(oldDungeon);
        oldDungeon = Instantiate(dungeon, parent);
        instance.newDungeon = true;
    }

}
