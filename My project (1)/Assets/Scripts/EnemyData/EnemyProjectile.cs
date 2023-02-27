using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    EnemyStats enemyStats;
    [HideInInspector]
    public int damage;          //damage that this bullet will deal (set by onObjectSpawn() in EnemyAttack.
    bool flag = false;          //flag used to make sure collider only hits once
    ObjectPooler objectPooler;

    private void Awake()
    {
        objectPooler = ObjectPooler.Instance;   //get the current object pooler
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        this.gameObject.SetActive(false);   //on collision, turn this object off.
        if (objectPooler != null)   //make sure the object pooler still exists.
        {
            objectPooler.poolDictionary["EnemyBulletPooler"].Enqueue(this.gameObject);  //add this object to the pooler
        }

        if (collision.gameObject.GetComponent<PlayerStats>() != null && flag == false)  //if the collision object has player stats and this object hasnt already hit the player.
        {
            flag = true;    //this bullet has already hit the player. prevents multiple hits from the same bullet(frame lag)
            collision.GetComponent<PlayerStats>().TakeDamage(damage); //deal damage
            flag = false;   //reset the flag.
        }
    }
}
