using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public string destinationScene;
    public GameObject spawnPlayer;
    GameManager gameManager;
    SpawnPlayer playerSpawner;
    GameObject[] poolers;
    ObjectPooler pooler;

    public int areaNumber;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        playerSpawner = spawnPlayer.GetComponent<SpawnPlayer>();
    }

    private void Start()
    {
        gameManager = GameManager.instance;
        poolers = GameObject.FindGameObjectsWithTag("Pooler");
        pooler = poolers[0].GetComponent<ObjectPooler>();
        playerSpawner.SpawnsPlayer();
        pooler.Begin();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            gameManager.currentArea = areaNumber;
            LoadScene();
        }
    }

    private void LoadScene()
    {
        
        if (destinationScene != null)
        {
            SceneManager.LoadScene(destinationScene);
        }
    }
}
