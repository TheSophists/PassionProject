using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    public Transform spawnPoint;
    public EnemyDropTable dropTable;
    [HideInInspector] public InventoryItemData[] drops;

    private void Start()
    {
        if(dropTable != null)
        {
            drops = dropTable.drops;    //gets the drops from the drop table Scriptable Object attached to this script
        }
    }


    public virtual void PickItem()
    {
        int somethingRoll = Random.Range(0, 100);       //Random used to pick from the array of drops
        if(somethingRoll <= dropTable.somethingRate)     //the something rate is used to calculate the % chance of receiving an item, any item. 
                                                        //so a monster with a somethingRate of 25 will drop something 25% of the time
        {
            int pickAmount = Random.Range(1, dropTable.maxAmount + 1);  //pick the quantity
            int dropsRNG = Random.Range(0, drops.Length);               //pick which element in the array will be dropped

            for (int i = 0; i < pickAmount; i++)                        //used to loop for the quantity of items to drop.
            {
                Instantiate(drops[dropsRNG].prefab, (spawnPoint.transform.position), Quaternion.identity);        //$$$LOOT$$$
            }
        }
    }
}
