using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyStats : CharacterStats
{
    CharacterStats charStats;
    Text text;
    public Transform location;
    public bool respawns;
    public GameObject EnemyGO;
    GameObject disabledEnemy;
    public EnemyRespawn respawn;
    public DropItem dropItem;
    public int respawnTimer;

    public void Start()
    {
        if (GameObject.Find("EnemyHealthBox/CurrentHealth").GetComponent<Text>())           //this block locates the text box needed to display enemy health
        {                                                                                    //will most likely be removed.
            text = GameObject.Find("EnemyHealthBox/CurrentHealth").GetComponent<Text>();
        }

        charStats = GetComponent<CharacterStats>();     //character stats for damage

        if (respawns == false)      //is the enemy supposed to respawn?
        {
            EnemyGO = null;     //if no, we dont need a gameobject.
        }
        else
        {
            disabledEnemy = Instantiate(EnemyGO, new Vector2(location.position.x, location.position.y), Quaternion.identity, location);     //create an enemy at the spawn location.
            disabledEnemy.SetActive(false);                                                                                                 //then disable the enemy until the active one dies. (EnemyRespawn)
        }
    }

    public override void Die()          //overriding characterStats Die()
    {
        if (respawns == true)
        {
            respawn.Respawn(disabledEnemy, respawnTimer);       //call Respawn in the EnemyRespawn class
            Destroy(gameObject);                                //destroy the currently active game object (the disabled object still exists)
        }
        else
        {
            Destroy(gameObject);        //if it doesnt respawn, simply destroy the enemy.
        }
        if(dropItem != null)            //if the enemy can drop items.
        {
            dropItem.PickItem();        //go pick and item to drop from their drop table.
        }

        //add death animation (enemies)
        
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);            //call the character Stats class's take damage.
        if(charStats.currentHealth > 0)     //if the enemy has health still
        {
            text.text = charStats.currentHealth.ToString();     //display the health value in the text box
        }
        else
        {
            text.text = "";     //otherwise there should be no text in the box.
        }
        
    }
}
