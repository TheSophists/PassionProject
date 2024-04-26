using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class DropChestItem : MonoBehaviour
{
    public Animator animator;
    public int coinAmount;
    public Rigidbody2D coinRB;

    public EnemyDropTable dropTable;
    [HideInInspector] public InventoryItemData[] drops;
    public Transform spawnPoint;
    Rigidbody2D coinClone;

    private void Start()
    {
        if (dropTable != null)
        {
            drops = dropTable.drops;    //gets the drops from the drop table Scriptable Object attached to this script
        }
    }

    public void PickItem()
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
        StartCoroutine(LaunchCoins());
    }

    public IEnumerator LaunchCoins()
    {
        for (int i = 0; i < coinAmount; i++)
        {
            coinClone = Instantiate(coinRB, transform.position, Quaternion.identity);
            coinClone.constraints = RigidbodyConstraints2D.None;
            coinClone.constraints = RigidbodyConstraints2D.FreezeRotation;

            coinClone.AddForce(new Vector2(Random.Range(-2f, 2f), 5f).normalized * 10f, ForceMode2D.Impulse);
            yield return new WaitForSeconds(.2f);

            if (coinClone != null)
            {
                StartCoroutine(StopCoins(coinClone));
            }

        }
    }

    public IEnumerator StopCoins(Rigidbody2D coins)
    {
        Debug.Log("here");
        yield return new WaitForSeconds(1.7f);
        coins.constraints = RigidbodyConstraints2D.FreezePositionX;
    }
}
