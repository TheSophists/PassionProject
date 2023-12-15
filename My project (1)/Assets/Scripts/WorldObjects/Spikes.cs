using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public Transform spikeLocation;
    GameObject gameManager;
    GameObject[] gameManagerList;
    GameObject[] playerList;
    PlayerManager playerManager;
    GameObject player;
    PlayerStats playerStats;

    // Start is called before the first frame update
    void Update()
    {
        if (gameManagerList == null || playerList == null)
        {
            gameManagerList = GameObject.FindGameObjectsWithTag("GameManager");
            playerList = GameObject.FindGameObjectsWithTag("Player");

            if (playerList.Length == 1)
            {
                player = playerList[0];
                playerStats = player.GetComponent<PlayerStats>();
            }
            else if (playerList.Length >= 2)
            {
                Debug.Log("More than one player found");
            }


            if (gameManagerList.Length == 1)
            {
                gameManager = gameManagerList[0];
            }
            else if (gameManagerList.Length >= 2)
            {
                Debug.Log("More than one game manager found");
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (player != null && collision.tag == "Player")
        {
            playerStats.TakeDamageIgnoreArmor(1);
            player.transform.position = spikeLocation.position;
        }
    }
}
