using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropChestItem : DropItem
{
    public Animator animator;
    public override void PickItem()
    {
        int pickAmount = Random.Range(1, dropTable.maxAmount + 1);  //pick the quantity
        int dropsRNG = Random.Range(0, drops.Length);               //pick which element in the array will be dropped

        for (int i = 0; i < pickAmount; i++)                        //used to loop for the quantity of items to drop.
        {
            GameObject item = Instantiate(drops[dropsRNG].prefab, (spawnPoint.transform.position), Quaternion.identity);        //$$$LOOT$$$
            Rigidbody2D itemRB = item.GetComponent<Rigidbody2D>();
            itemRB.constraints = RigidbodyConstraints2D.FreezeAll;
            if (animator != null)
            {
                animator.SetBool("Loot", true);
            }
        }
    }
}
