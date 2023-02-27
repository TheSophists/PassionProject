using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NPCData", menuName = "My Game/NPC Data")]
public class NPCData : ScriptableObject
{
    public string[] dialog;
}
