using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "My Game/Enemy Drop Table")]
public class EnemyDropTable : ScriptableObject
{
    public InventoryItemData[] drops;   //an array of game items that make up possible drops.
    public int maxAmount;             //the highest quantity of the items that can be dropped.
    public int somethingRate;         //roll > nothingRate = no drop.
                                      //the rate is the % chance of receiving an item, any item. so a somethingRate of 75 is a 75% chance to get an item.
}
