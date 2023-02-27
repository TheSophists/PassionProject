using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    private void Awake()
    {
        {
            instance = this;        //this is a singleton
        }
    }

    public GameObject player;
    public Text coinText;
    public int coins;
    public void KillPlayer()
    {
        SceneManager.LoadScene("PlayerDied");
    }

}
