using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDistEnemies : MonoBehaviour
{

    //will be used to make all enemies spawn at the start of the scene.
    //this allows me to customize the amount and types of enemy that spawn.
    public GameObject turretGO;
    public GameObject flyingMeleeGO;
    public GameObject flyingShootGO;
    public GameObject groundedMeleeGO;
    public GameObject groundedShootGO;

    Transform turretLocations;
    Transform flyingShootLocations;
    Transform flyingMeleeLocations;
    Transform groundedShootLocations;
    Transform groundedMeleeLocations;

    public List<Transform> flyingShootList;
    public List<Transform> groundedMeleeList;
    public List<Transform> groundedShootList;
    public List<Transform> turretList;
    public List<Transform> flyingMeleeList;

    GameManager gameManager;
    DifficultyData difficulty;

    public void Start()
    {
        gameManager = GameManager.instance;
        difficulty = gameManager.DetermineDifficulty();
        Debug.Log(difficulty.flyingMeleeQuantity);

        foreach (Transform child in transform)
        {
            if (child.name == "TurretLocations")
            {
                int i = 0;
                turretLocations = child;
                foreach (Transform child2 in turretLocations)
                {
                    turretList.Add(child2);
                    //Debug.Log(turretList[i].name);
                    i++;
                }
                PickAndSpawn(turretList, difficulty, turretGO, "Turret");
            }

            else if (child.name == "FlyingMeleeLocations")
            {
                int i = 0;
                flyingMeleeLocations = child;
                foreach (Transform child2 in flyingMeleeLocations)
                {
                    flyingMeleeList.Add(child2);
                    //Debug.Log(flyingMeleeList[i].name);
                    i++;
                }
                PickAndSpawn(flyingMeleeList, difficulty, flyingMeleeGO, "FlyingMelee");
            }

            else if (child.name == "FlyingShootLocations")
            {
                int i = 0;
                flyingShootLocations = child;
                foreach (Transform child2 in flyingShootLocations)
                {
                    flyingShootList.Add(child2);
                    //Debug.Log(flyingShootList[i].name);
                    i++;
                }
                PickAndSpawn(flyingShootList, difficulty, flyingShootGO, "FlyingShoot");
            }

            else if (child.name == "GroundedShootLocations")
            {
                int i = 0;
                groundedShootLocations = child;
                foreach (Transform child2 in groundedShootLocations)
                {
                    groundedShootList.Add(child2);
                    //Debug.Log(groundedShootList[i].name);
                    i++;
                }
                PickAndSpawn(groundedShootList, difficulty, groundedShootGO, "GroundedShoot");
            }

            else if (child.name == "GroundedMeleeLocations")
            {
                int i = 0;
                groundedMeleeLocations = child;
                foreach (Transform child2 in groundedMeleeLocations)
                {
                    groundedMeleeList.Add(child2);
                    //Debug.Log(groundedMeleeList[i].name);
                    i++;
                }
                PickAndSpawn(groundedMeleeList, difficulty, groundedMeleeGO, "GroundedMelee");
            }
        }
    }

    public void PickAndSpawn(List<Transform> locationList, DifficultyData data, GameObject enemyPrefab, string enemyType)
    {
        int numberOfSpawns = 0;
        switch (enemyType)
        {
            case "Turret":
                numberOfSpawns = data.turretQuantity; break;
            case "FlyingMelee":
                numberOfSpawns = data.flyingMeleeQuantity; break;
            case "FlyingShoot":
                numberOfSpawns = data.flyingShootQuantity; break;
            case "GroundedShoot":
                numberOfSpawns = data.GroundedShootQuantity; break;
            case "GroundedMelee":
                numberOfSpawns = data.GroundedMeleeQuantity; break;
        }


        for(int i = 0; i < numberOfSpawns; i++)
        {
            int locationValue = Random.Range(0, locationList.Count - 1);
            Instantiate(enemyPrefab, locationList[locationValue], false);
            locationList.RemoveAt(locationValue);
        }
    }

}
