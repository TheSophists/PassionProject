using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBossAI : MonoBehaviour
{
    EnemyAttack enemyAttack;
    Vector2 enemyDirection;
    PlayerManager playerManager;
    GameObject player;
    public Transform enemyGFX;
    bool m_FacingRight = true;
    Rigidbody2D bossRB;
    bool flag = false;
    public GameObject flash;
    bool teleportFlag = false;
    public Transform[] teleportLocations;
    public Transform[] enemySpawns;
    public GameObject[] enemies;


    void Start()
    {
        enemyAttack = GetComponent<EnemyAttack>();      //get components and instances needed
        playerManager = PlayerManager.instance;

        player = playerManager.player;              //find the player
        bossRB = this.gameObject.GetComponent<Rigidbody2D>();

        //blink thrice before teleporting at the start.
        for (int i = 0; i < 3; i++)
        {
            StartCoroutine("AttackAnimation");  //start the animation
        }
    }

    private void FixedUpdate()
    {   //this is the direction used to shoot
        enemyDirection = (new Vector2(player.transform.position.x, player.transform.position.y) - new Vector2(enemyGFX.position.x, enemyGFX.position.y)).normalized;

        //this will make the boss face toward the player based on their location just aftr the teleport.
        if (enemyDirection.x > 0 && !m_FacingRight)
        {
            Flip();
        }
        else if (enemyDirection.x < 0 && m_FacingRight)
        {
            Flip();
        }

        //boolean flag to prevent the attack coroutine from being called before it has finished.
        if (enemyAttack.isRunning == false && !flag) //checks if the shooting coroutine in the class enemyAttack is currently running, ie the enemy is currently shooting. also for the animation coroutine.
        {

            for (int i = 0; i < 3; i++) //the flags determine it is time to attack, so we will shoot 3 times before teleporting to a new location.
            {
                StartCoroutine("AttackAnimation");
            }
            if (teleportFlag == false)  //the flag is used to ensure that the bossTeleport coroutine is not currently running.
            {
                teleportFlag = true;        //flip the flag.
                StartCoroutine("BossTeleport"); //start the teleport.
            }

        }

        //this will make the boss teleport to 1 of 4 locations within the room.

    }

    public IEnumerator BossTeleport()
    {
        int rng = Random.Range(0, 4);                                       //used to pick 1 of 4 possible teleport locations.

        this.gameObject.transform.position = teleportLocations[rng].transform.position; //move the boss game object to the teleport location. 

        Instantiate(enemies[rng], enemySpawns[rng]);    //spawn a minon enemy associated with that teleport location.
        yield return new WaitForSeconds(9);             //wait before being able to teleport again.
        teleportFlag = false;                           //flip the flag back so the coroutine can be called again. 
    }

    public IEnumerator AttackAnimation()
    {
        flag = true;    //this coroutine is currently running.

        for (int i = 0; i < 3; i++) //we will flash the attack indicator 3 times. 
        {
            flash.SetActive(true);
            yield return new WaitForSeconds(.2f);
            flash.SetActive(false);
            yield return new WaitForSeconds(.2f);
        }

        //after the animation, we will actually shoot a projectile.
        enemyAttack.StartCoroutine("Shoot");
        //flip the flag back so the coroutine can be called again. 
        flag = false;
    }

    public void Flip()
    {
        //this will simply flip the boss's sprite by multiplying the local x scale by -1 (always flips)
        bossRB.constraints = RigidbodyConstraints2D.FreezePosition;
        bossRB.constraints = RigidbodyConstraints2D.None;
        bossRB.constraints = RigidbodyConstraints2D.FreezeRotation;

        m_FacingRight = !m_FacingRight;

        // Multiply the enemy's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
