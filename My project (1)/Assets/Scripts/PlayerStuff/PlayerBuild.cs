using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PlayerStats))]
public class PlayerBuild : MonoBehaviour
{
    GameObject builds;
    GameObject currentBuilds;
    GameObject player;
    Rigidbody2D playerRB;
    Equipment buildData;
    EquipmentManager equipManager;

    Player_Movement playerController;

    PlayerStats myStats;

    bool direction;

    float buildOffset;
    int buildTimer;
    bool isRunning = false;

    private void Start()
    {
        player = PlayerManager.instance.player;
        playerRB = player.GetComponent<Rigidbody2D>();

        equipManager = EquipmentManager.instance;

        playerController = player.GetComponent<Player_Movement>();

        myStats = GetComponent<PlayerStats>();

    }

    public void Update()
    {
        if (equipManager.GetBuilds() != null && buildData != equipManager.GetBuilds())
        {
            buildData = equipManager.GetBuilds();
            builds = buildData.buildsPrefab;
            buildTimer = myStats.buildTimer.GetValue();
        }


        if (playerController != null)
        {
            direction = playerController.playerDirection;
        }


        if (direction && buildData != null)
        {
            buildOffset = buildData.buildOffset;
        }
        else if (buildData != null)
        {
            buildOffset = -buildData.buildOffset;
        }

        if (!isRunning && Input.GetKeyDown(KeyCode.F) && builds != null)
        {
            StartCoroutine(Build());
        }

        Destroy(currentBuilds, ((float)buildTimer/100));

    }

    IEnumerator Build()
    {
        isRunning = true;
        Debug.Log(currentBuilds);
        currentBuilds = Instantiate(builds, new Vector3(playerRB.transform.position.x + buildOffset, playerRB.transform.position.y - 1.5f, 0f), Quaternion.identity);
        Debug.Log("Build Cooldown... " + myStats.buildCoolDown.GetValue() + " second cooldown");
        yield return new WaitForSeconds((float)myStats.buildCoolDown.GetValue()/100);
        isRunning = false;
    }
}
