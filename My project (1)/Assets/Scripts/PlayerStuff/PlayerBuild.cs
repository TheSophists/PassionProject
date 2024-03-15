using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PlayerStats))]
public class PlayerBuild : MonoBehaviour
{
    GameObject builds;
    GameObject currentBuilds;//stores gameobject of the builds that are equipped.

    GameObject player;
    Rigidbody2D playerRB;

    Equipment buildData;
    EquipmentManager equipManager;

    CharacterController2D playerController;

    PlayerStats myStats;

    bool direction; //direction the player is facing

    float buildOffsetX;  //X-axis offset of this particular build.
                         //*** may need an offset for the Y axis as well.***
    int buildTimer; //length of time before the player can build again.


    bool isRunning = false;

    private void Start()
    {
        player = PlayerManager.instance.player;         //find the player
        playerRB = player.GetComponent<Rigidbody2D>();  //get the players Rigid Body.

        equipManager = EquipmentManager.instance;       //get current equipment.

        playerController = player.GetComponent<CharacterController2D>();  //get the player controller to find the players current direction. 

        myStats = GetComponent<PlayerStats>();      //get player stats. 

    }

    public void Update()
    {
        if (equipManager.GetBuilds() != null && buildData != equipManager.GetBuilds())      //if the equipment manager has build data and the player recently equipped a new build type.
        {
            buildData = equipManager.GetBuilds();       //replace the stored build data/prefab/timer used
            builds = buildData.buildsPrefab;
            buildTimer = myStats.buildTimer.GetValue(); //this is how long the object is instantiated for.
        }


        if (playerController != null)   //get the direction of the player (to determine which side to instantiate the object on.
        {
            direction = playerController.m_FacingRight;
        }


        if (direction && buildData != null)     //if current direction is true and build data exists. 
        {
            buildOffsetX = buildData.buildOffset;
        }
        else if (buildData != null)             //if current direction is false and build data exists.
        {
            buildOffsetX = -buildData.buildOffset;
        }

        if (!isRunning && Input.GetKeyDown(KeyCode.F) && builds != null)    //if the coroutine is not currently running and the player presses the build key, as well as having prefab stored in builds
        {
            StartCoroutine(Build());
        }

        Destroy(currentBuilds, ((float)buildTimer/100));    //destroy the instantiated object (while keeping "build" data safe)  after a certain amount of time
                                                            //using x/100 to allow for decimal times. 

                                                            //*********Might need to rework this for better results***********

    }

    IEnumerator Build()
    {
        isRunning = true;   //flag to prevent the coroutine from executing twice. 

        Debug.Log(playerRB.transform.position.x + " " + playerRB.transform.position.y);
        currentBuilds = Instantiate(builds, new Vector3(playerRB.transform.position.x + buildOffsetX, playerRB.transform.position.y, 0f), Quaternion.identity);
        //Spawns the build prefab in a new variable "currentBuilds" which is the active in game object. We spawn it based on the location of the player, adding in extra offsets depepnding on the type of build it is.
        //Some builds may need to go above, below, or behind the player.
        //******May need to offset the Y axis as well at some point.*********

        Debug.Log("Build Cooldown... " + myStats.buildCoolDown.GetValue() + " second cooldown");//used for testing purposes

        yield return new WaitForSeconds((float)myStats.buildCoolDown.GetValue()/100); //prevents the coroutine from being called again before the object has been destroyed.
                                                                                      //this time is determined by the build cooldown variable
        isRunning = false;  //flip the flag to allow the coroutine to run again.
    }
}
