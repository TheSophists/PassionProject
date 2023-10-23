using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;   //get the current player.

    public GameObject playerPrefab;   //player's prefab 
    GameObject[] playerCheck;          //used to make sure there arent duplicate players
    public GameObject player;       //players game object that isnt destroyed.
    public Text coinText;       //Text that shows the number of coins that the player has collected.
    public int coins;           //the number of coins that is applied to the coinText box.

    private void Awake()
    {
        instance = this;        //this is a singleton used to make sure we are only tracking a single instance of the player manager.
    }

    private void Start()
    {
        playerCheck = GameObject.FindGameObjectsWithTag("Player");
        if(playerCheck.Length == 1)
        {
            player = playerCheck[0];
        }
    }


    public void KillPlayer()    //this is the function that is called when the player dies. can be overwritten for different functions after progress has been made. 
    {
        SceneManager.LoadScene("PlayerDied");
    }

}
