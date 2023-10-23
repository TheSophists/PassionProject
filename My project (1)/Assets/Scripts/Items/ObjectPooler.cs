using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool       //define what a pool is
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }


    public static ObjectPooler Instance;        //singleton creation

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(Instance);
    }                                           //singleton creation

    public List<Pool> pools;                    //list containing different pools. (1 for enemy bullets, 1 for player bullets.
    public Dictionary<string, Queue<GameObject>> poolDictionary;    //dictionary with a string and a queue. The string contains the name for the pool, and a queue of game objects to be spawned.

    // Start is called before the first frame update
    void Start()
    {
        /*poolDictionary = new Dictionary<string, Queue<GameObject>>();       //set the pool dictionary to a new dictionary containing a tag and object queue.

        foreach(Pool pool in pools)                                         //for each pool that we've created.
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();         //make a new queue for the gameobjects.

            for (int i = 0; i < pool.size; i++)         //for each object in the pool
            {
                GameObject obj = Instantiate(pool.prefab);  //create a gameobject by instantiating the prefab associated for each pool
                obj.SetActive(false);                       //set it as inactive until we are ready to use it
                objectPool.Enqueue(obj);                    //add that object to the objectPool Queue
            }
            poolDictionary.Add(pool.tag, objectPool);       //store this tag and the new Queue (a new pooler) in the pool dictionary.
        }*/
    }

    public void Begin()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();       //set the pool dictionary to a new dictionary containing a tag and object queue.

        foreach (Pool pool in pools)                                         //for each pool that we've created.
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();         //make a new queue for the gameobjects.

            for (int i = 0; i < pool.size; i++)         //for each object in the pool
            {
                GameObject obj = Instantiate(pool.prefab);  //create a gameobject by instantiating the prefab associated for each pool
                obj.SetActive(false);                       //set it as inactive until we are ready to use it
                objectPool.Enqueue(obj);                    //add that object to the objectPool Queue
            }
            poolDictionary.Add(pool.tag, objectPool);       //store this tag and the new Queue (a new pooler) in the pool dictionary.
        }
    }

    public GameObject SpawnFromPool(string tag, Vector2 position, Quaternion rotation)     //used to spawn the object when we need it
    {
        if (!poolDictionary.ContainsKey(tag))       //if we do not have a pool that matches the tag thats being requested.
        {
            Debug.LogWarning("Pool with tag " + tag + " does not exist");   //give a warning and do nothing.
            return null;
        }
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();       //take the first item in the queue, and remove it from the queue.

        objectToSpawn.SetActive(true);                              //set it as active, as its ready to be fired
        objectToSpawn.transform.position = position;                //set the position/rotation based on data being passed. (typically the location/rotation that the enemy/player fired from)
        objectToSpawn.transform.rotation = rotation;

        IPooledObject pooledObj = objectToSpawn.GetComponent<IPooledObject>();      //get the IpooledObject component. I believe that component is currently lacking a function, beyond being a potential listener.

        if (pooledObj != null)
        {
            pooledObj.OnObjectSpawn();      //does something when the object is spawned, doesnt currently do anything
                                            //**********************************************************************
        }
        return objectToSpawn;   //return the spawned object.
    }
}


