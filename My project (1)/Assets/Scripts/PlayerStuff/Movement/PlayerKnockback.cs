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

    private void Start()
    {
        player = PlayerManager.instance;    //get the instance of the player.
        playerRb = player.player.GetComponent<Rigidbody2D>();   //get the players RigidBody2D
    }


    public void PlayerHit(GameObject enemy)
    {
        Vector2 knockBack = -(player.transform.position - enemy.transform.position);    //get a Vector2 from the enemy's position to the player's position.
        playerRb.AddForce(((knockBack.normalized) *50f +  new Vector2(0,10)), ForceMode2D.Impulse);     //Adds the force to player based on the direction calculated above. 
    }
}
