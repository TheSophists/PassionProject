using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//this Script is attached to the melee box object that is a child of the player Gameobject. 
public class PlayerMelee : MonoBehaviour
{
    public BoxCollider2D meleeBox;      //the hitbox that melee attacks use
    PlayerManager playerManager;
    EquipmentManager equipmentManager;
    PlayerStats playerStats;

    int meleeDamage;
    Equipment MeleeData;
    bool flag = false;

    public GameObject weaponPart;
    Vector2 direction;
    float angle;
    float knockback = 10f;

    public GameObject meleeAnimation;


    private void Start()
    {
        playerManager = PlayerManager.instance;     //get data regarding the player, equipment, and player stats.
        equipmentManager = EquipmentManager.instance;
        playerStats = playerManager.player.GetComponent<PlayerStats>();
        meleeBox.enabled = false;
        //keep the melee box disabled until the player presses the right key. 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 weaponPointPosition = new Vector2(weaponPart.transform.position.x, weaponPart.transform.position.y);    //get the point of the weapon that projectiles will fly out of.
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);  //get the current mouse position in world space.
        //subtract these values to get a new direction vector used for the melee attack direction. 
        direction = (mousePosition - weaponPointPosition);

        //get the angle that the projectiles should be instantiated with.
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        meleeBox.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        meleeDamage = playerStats.meleeDamage.GetValue();   //get the amount of damage that a melee attack will do.

        
        if (Input.GetButton("Melee") && flag == false)  //if the player tries to melee attack and the cooldown timer has already expired. 
        {
            StartCoroutine(MeleeAnimation());   //this coroutine will visually display the hitbox of the melee attack.
            flag = true;    //before starting the 
            StartCoroutine(MeleeOn());  //start the actual attack functions
        }

    }

    //when the player hits an enemy with their melee hit box.
    public void OnTriggerEnter2D(Collider2D collision)  
    {
        if(collision.tag == "Enemy")    //if it hits an enemy.
        {
            EnemyStats enemyStats = collision.GetComponent<EnemyStats>();   //get stats and info about their Rigid body
            Rigidbody2D enemyRB = collision.GetComponent<Rigidbody2D>();


            enemyRB.AddForce(direction.normalized * knockback, ForceMode2D.Impulse); //add a knockback force based on direction calculated earlier. 

            //make the enemy take damage. 
            enemyStats.TakeDamage(meleeDamage);
        }
    }

    IEnumerator MeleeOn()
    {
        //this function turns on the melee box once a player has pressed the proper key. The box stay enabled for .1 seconds, then turns off. The coroutine
        //then waits an additional 1 second before turning the flag back to false. This prevent the coroutine from executing multiple times/infinitely. 
        meleeBox.enabled = true;

        yield return new WaitForSeconds(.1f);

        meleeBox.enabled = false;

        yield return new WaitForSeconds(1f);

        flag = false;
    }

    //provides visuals for melee attacks. This will change as I develop animations. 
    IEnumerator MeleeAnimation()
    {
        meleeAnimation.SetActive(true);

        yield return new WaitForSeconds(.1f);

        meleeAnimation.SetActive(false);

    }
}
