using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    GameObject projectile;
    PlayerStats playerStats;
    ObjectPooler objectPooler;

    PlayerManager playerManager;

    bool flag = false; //flag used to make sure collider only hits once

    private void Start()
    {
        playerManager = PlayerManager.instance; //player manager instance
        playerStats = playerManager.player.GetComponent<PlayerStats>();     //get info about the player's stats

        objectPooler = ObjectPooler.Instance;   //bullet pooler instance

        projectile = this.gameObject;   //the projectile is whatever this script is attached to
    }

    private void Update()
    {
        float intX = Mathf.Abs(playerManager.player.transform.position.x - this.gameObject.transform.position.x);           //these are the distances from the player to the projectile object.
        float intY = Mathf.Abs(playerManager.player.transform.position.y - this.gameObject.transform.position.y);           //this is used to destroy projectiles that haven't hit anything and replace them in the pooler.

        //if the bullet moves more than 100 units in any direction.
        if (intX > 100 || intY > 100)
        {

            projectile.SetActive(false);    //then disable the projectile.

            if (objectPooler != null)
            {
                objectPooler.poolDictionary["BulletPooler"].Enqueue(projectile);    //and place the projectile back in the pooler.
            }
        }
    }

    //when a projectile hits any object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        projectile.SetActive(false);    //disable the projectile.

        if (objectPooler != null)
        {
            objectPooler.poolDictionary["BulletPooler"].Enqueue(projectile);    //add the projectile back into the queue
        }

        if (collision.gameObject.GetComponent<EnemyStats>() != null && flag == false)   //if the object the projectile collided with has an enemy stats component (AND the flag used to indicate being hit hasnt been flipped yet)
        {
            flag = true;        //flip the flag to prevent a single projectile from damaging the enemy in multiple frames.

            EnemyDamaged(collision.GetComponent<EnemyStats>(), playerStats.damage.GetValue());  //we hit an enemy, so next we deal with the health stat changes 
        }
        flag = false;   //flip the flag back so that when it is re-queued it is still able to damage enemies.
    }

    public void EnemyDamaged(CharacterStats takeDamage, int damage)
    {
        takeDamage.TakeDamage(damage);  //call the takeDamage function in the CharacterStats class.
    }
}
