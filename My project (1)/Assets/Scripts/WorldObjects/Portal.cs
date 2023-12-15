using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public string destinationScene;
    public GameObject spawnPlayer;
    SpawnPlayer playerSpawner;
    GameObject[] poolers;
    ObjectPooler pooler;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        playerSpawner = spawnPlayer.GetComponent<SpawnPlayer>();
    }

    private void Start()
    {
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
        LoadScene();
    }

    private void LoadScene()
    {
        
        if (destinationScene != null)
        {
            SceneManager.LoadScene(destinationScene);
        }
    }
}
