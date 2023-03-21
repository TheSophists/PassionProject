using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;   //get the current player.

    private void Awake()
    {
        {
            instance = this;        //this is a singleton used to make sure we are only tracking a single instance of the player manager.
        }
    }

    public GameObject player;   //player's GO
    public Text coinText;       //Text that shows the number of coins that the player has collected.
    public int coins;           //the number of coins that is applied to the coinText box.

    public void KillPlayer()    //this is the function that is called when the player dies. can be overwritten for different functions after progress has been made. 
    {
        SceneManager.LoadScene("PlayerDied");
    }

}
