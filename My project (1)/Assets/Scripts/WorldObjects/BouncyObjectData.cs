using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WorldObjectData", menuName = "My Game/WorldObjectData")]       //menu entry creation for this data type.
public class BouncyObjectData : ScriptableObject
{
    public float thrust;    //thje amount of thrust that is applied to a character when it collides with a bouncy object.
}
