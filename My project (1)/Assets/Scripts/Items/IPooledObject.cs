using UnityEngine;

public interface IPooledObject
{
    void OnObjectSpawn();       //calls this function in various places, depending on which type of pool its called from
                                //players and enemies have 2 different OnObjectSpawn functions for their poolers
}                               //as they shoot in 2 different ways. 