using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitiateCombat : MonoBehaviour
{
    /*TurnManager turnManager;
    PlayerManager playerManager;
    GameObject player;
    PlayerStats playerStats;
    [SerializeField]
    public List<CharacterStats> combatList = new List<CharacterStats>();
    GameObject parent;
    bool flag = false; //used to ensure collisions only happen once
    private Dictionary<CharacterStats, int> turnOrderDictionary;


    public void Start()
    {
        parent = this.gameObject;
        turnManager = TurnManager.turnInstance;     //get an instance of the turn manager.
        playerManager = PlayerManager.instance;     //get instance of the player Manager
        player = playerManager.player;
        playerStats = player.GetComponent<PlayerStats>(); 
        turnOrderDictionary = new Dictionary<CharacterStats, int>();
    }


    public void OnTriggerEnter2D(Collider2D collision)     //on entering the area, turn combat on
    {
        if (collision.CompareTag("Player") && flag == false)
        {
            flag = true;
            MakeCombatLists();
            turnManager.StartCombat();
        }
    }

    public void OnTriggerExit2D(Collider2D collision)       //upon leaving the area, turn combat off.
    {
        if (collision.CompareTag("Player") && flag == true)
        {
            flag = false;
            turnManager.EndCombat();
            combatList.Clear();
        }
    }

    public void MakeCombatLists()        //returns a list of game objects that are currently in combat with the player
    {
        combatList.Add(playerStats);
        turnOrderDictionary.Add(playerStats, playerStats.initiative.GetValue());

        foreach (Transform child in parent.transform)    //find the children of the object this script is attached to (empty object under A*)
        {
            foreach (Transform child2 in child.transform)   //find the children of that child.
            {
                if (child2.CompareTag("Enemy"))      //if the child has an enemy tag
                {
                    if (child2.GetComponent<EnemyStats>() != null)
                    {
                        EnemyStats enemyStats = child2.GetComponent<EnemyStats>();

                        combatList.Add(enemyStats); //add it to the list of enemies that are in combat.
                        turnOrderDictionary.Add(enemyStats, enemyStats.initiative.GetValue());
                    }
                }
            }
        }

        turnManager.SortLists(combatList, turnOrderDictionary);
    }*/
}
