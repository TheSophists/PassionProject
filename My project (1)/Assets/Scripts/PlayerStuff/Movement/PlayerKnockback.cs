using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterStats))]
public class PlayerAttacked : MonoBehaviour
{
    bool invuln = false;
    PlayerManager player;
    Rigidbody2D playerRb;
    CharacterStats myStats;

    private void Start()
    {
        player = PlayerManager.instance;
        playerRb = player.player.GetComponent<Rigidbody2D>();

        myStats = GetComponent<CharacterStats>();
    }


    public void PlayerHit(GameObject enemy)
    {

        if (invuln == false)
        {
            CharacterCombat playerCombat = enemy.GetComponent<CharacterCombat>();
            playerCombat.Attack(myStats);
        }

        Vector2 knockBack = -(player.transform.position - enemy.transform.position);
        playerRb.AddForce(((knockBack.normalized) *50f +  new Vector2(0,10)), ForceMode2D.Impulse);     //deals with knockback


        if (invuln == false)
        {
            invuln = true;
            Invoke("resetInvuln", 1);
        }
    }


    void resetInvuln()
    {
        invuln = false;
    }
}
