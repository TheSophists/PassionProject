using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawn : MonoBehaviour
{
    public void Respawn(GameObject disabledEnemy, int spawnTimer)
    {
        StartCoroutine(SpawnEnemy(disabledEnemy, spawnTimer));      //DO NOT REMOVE
                                                                    //for some reason, trying to call a coroutine directly from Enemy Stats doesnt work.
                                                                    //i belioeve its because the game object is deleted immediately after calling Respawn().
    }

    public IEnumerator SpawnEnemy(GameObject disabledEnemy, int spawnTimer)
    {
        yield return new WaitForSeconds(spawnTimer);        //wait before setting the disabled enemy active
        disabledEnemy.SetActive(true);                      //the disabled enemy is created and turned off as the current enemy is turned on, when an enemy dies, theyre destroyed
    }                                                       //and the disabled enemy that was created when the enemy was set active, is set active itself.
                                                            //instantiate -> create enemy at spawn location -> disable enemy -> when original enemy dies, set this one active after a delay.

}
