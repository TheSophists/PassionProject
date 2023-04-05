using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NPCData", menuName = "My Game/NPC Data")]      //creates a menu entry for creating this type of object.

public class NPCData : ScriptableObject     //this Scriptable Object is used for friendly npc's currently
{
    public string[] dialog;     //as of right now, the only thing this type of npc can do is speak. There will most likely be code related to crafting/quests/trading added here eventually. 
}
