using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPCData", menuName = "My Game/Visuals Data")]  //createss menu option for this type of scriptable object.
public class VisualsData : ScriptableObject
{
    public GameObject visuals;  //the sprite that will be loaded onto the character. 
}
