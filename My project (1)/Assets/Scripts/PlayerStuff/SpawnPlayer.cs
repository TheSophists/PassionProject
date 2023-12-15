using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject gameManager;
    GameObject[] gameManagerList;
    GameObject[] playerSpawnLocation;
    GameObject[] playerCheck;
    GameObject player;
    GameObject playerRB;
    GameObject[] checkObjectPooler;
    public GameObject objectPoolerPrefab;
    GameObject objectPoolerGO;
    ObjectPooler objectPooler;

    private void Awake()
    {
        SpawnGameManager();
        SpawnObjectPooler();
    }

    public void SpawnObjectPooler()
    {
        checkObjectPooler = GameObject.FindGameObjectsWithTag("Pooler");

        if (checkObjectPooler.Length > 1)
        {
            Debug.Log("More than one object pooler found");
        }
        if(checkObjectPooler.Length == 0)
        {
            objectPoolerGO = Instantiate(objectPoolerPrefab);
            objectPooler = objectPoolerGO.GetComponent<ObjectPooler>();
        }
    }

    public void SpawnGameManager()
    {
        gameManagerList = GameObject.FindGameObjectsWithTag("GameManager");

        if (gameManagerList.Length > 1)
        {
            Debug.Log("More than one game manager found");
        }
        if (gameManagerList.Length == 0)
        {
            Instantiate(gameManager);
        }
    }

    public void SpawnsPlayer()
    {
        playerSpawnLocation = GameObject.FindGameObjectsWithTag("SpawnLocation");
        playerCheck = GameObject.FindGameObjectsWithTag("Player");


        if (playerSpawnLocation.Length > 1)
        {
            Debug.Log("more than one spawn found");
        }

        if (playerSpawnLocation.Length == 1)
        {
            Vector2 spawnLocation = new Vector2(playerSpawnLocation[0].transform.position.x, playerSpawnLocation[0].transform.position.y);
            if (playerCheck.Length == 0)
            {
                playerRB = PlayerManager.instance.playerPrefab;
                player = Instantiate(playerRB, spawnLocation, Quaternion.identity);
                DontDestroyOnLoad(player);
            }
            else if (playerCheck.Length == 1)
            {
                player = playerCheck[0];
                player.transform.position = new Vector2 (spawnLocation.x, spawnLocation.y);
            }
            else
            {
                Debug.Log("More than one player found");
            }
        }
    }
}
