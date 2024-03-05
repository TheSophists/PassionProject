using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DifficultyData", menuName = "My Game/Difficulty Data")]
public class DifficultyData : ScriptableObject
{
    public int turretQuantity;
    public int flyingShootQuantity;
    public int flyingMeleeQuantity;
    public int GroundedMeleeQuantity;
    public int GroundedShootQuantity;
}
