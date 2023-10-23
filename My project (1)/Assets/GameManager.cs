using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance; 
    
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;            //singleton that manages the equipment that is currently equipped
        }
        DontDestroyOnLoad(instance);
    }
}
