using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterStats))]
public class PlayerAttacked : MonoBehaviour
{
    bool invuln = false;        //flag used to set invulnerability after the player has been damaged.   
    PlayerManager player;
    Rigidbody2D playerRb;
    CharacterStats myStats;

    private void Start()
    {
        player = PlayerManager.instance;    //get the instance of the player.
        playerRb = player.player.GetComponent<Rigidbody2D>();   //get the players RigidBody2D

        myStats = GetComponent<CharacterStats>();   //get the stats of the player. 
    }


    public void PlayerHit(GameObject enemy)
    {

        if (invuln == false)    //if the player is not invulnerable. 
        {
            CharacterCombat playerCombat = enemy.GetComponent<CharacterCombat>();   //get the enemy's stats
            playerCombat.Attack(myStats);   //attack the player using the enemy's stats/damage
        }

        Vector2 knockBack = -(player.transform.position - enemy.transform.position);    //get a Vector2 from the enemy's position to the player's position.
        playerRb.AddForce(((knockBack.normalized) *50f +  new Vector2(0,10)), ForceMode2D.Impulse);     //Adds the force to player based on the direction calculated above. 


        if (invuln == false)    //if the player is not already invulnerable
        {
            invuln = true;  //make them invulnerable, as they have recently taken damage.
            Invoke("resetInvuln", 1);   //reset the invulnerable to false after 1 second has passed.
        }
    }


    void resetInvuln()
    {
        invuln = false; //coroutine that resets invuln after a set time. 
    }
}
