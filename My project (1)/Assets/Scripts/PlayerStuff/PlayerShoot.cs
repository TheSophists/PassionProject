using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEditor.Tilemaps;

public class PlayerShoot : MonoBehaviour, IPooledObject
{
    public GameObject projectile;
    public GameObject player;
    public Camera playerCam;
    PlayerStats myStats;
    PlayerManager playerManager;
    public Transform weaponPart; //location of weapon
    public Transform weaponPoint;

    public Transform firePoint;    //location of the point that projectiles are instantiated at.

    Equipment rangedData;   //info on the current weapon
    int rateOfFire;         //timer before player can shoot again.
    int bulletVel;            //this value will be used to set bullet velocity.
    EquipmentManager equipManager; //instance of the current equipment

    bool isShooting = false; //flag for the shooting coroutine.
    bool isReloading = false; //flag for reloading.

    int ammoAmount;
    int clipSize;
    int reloadTime;

    ObjectPooler objectPooler;  //pooler that stores all bullet GO's 

    Vector2 direction;  //direction that the player is currently aiming.
    float angle;        //angle that player is aiming in.

    bool equipFlag = false;
    bool facingRight;

    private void Start()
    {
        facingRight= true;
        //get singletons of equipment, bullet pooler
        equipManager = EquipmentManager.instance;
        objectPooler = ObjectPooler.Instance;
        playerManager = PlayerManager.instance;

        //get current stats
        myStats = playerManager.player.GetComponent<PlayerStats>();

        //if there is not a firepoint attached to the player.
        if (firePoint == null)
        {
            Debug.Log("HI");
        }
    }

    void FixedUpdate()
    {
        //get the fire point position on the weapon part of the character.
        Vector2 weaponPointPosition = new Vector2(weaponPoint.transform.position.x, weaponPoint.transform.position.y);

        //get the current mousse position in world space.
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);

        //subtract the above positions to get a Vector2 that points from the player to the mouse pointer.
        direction = (mousePosition - weaponPointPosition);

        //this is the angle that is calculated from direction that is used to set the angle that the projectiles will fly out at. The angle matches the direction the player is firing in
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        //this also moves the weapon part to match the direction that the player is firing.
        weaponPoint.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        if(direction.x > 0 && facingRight == false)
        {
            Flip();
        }
        if (direction.x < 0 && facingRight == true)
        {
            Flip();
        }

        //if there is no ranged data OR if the ranged data doesnt match the previous frame,
        //we update the data being used for firing (Equipment change check)
        if (equipManager.GetRanged() != null && rangedData != equipManager.GetRanged())
        {
            rangedData = equipManager.GetRanged();
            rateOfFire = myStats.rateOfFire.GetValue();
            bulletVel = myStats.bulletVelocity.GetValue();
            reloadTime = myStats.reloadTime.GetValue();
            clipSize = myStats.clipSize.GetValue();
            ammoAmount = clipSize;
            equipFlag = false;
        }
        //this is the default values for when the player has no ranged weapon equipped (this allows us to still fire without checking for data)
        //the flag is used to ensure these values are not set every frame when the player is using the default weapon
        else if (equipManager.GetRanged() == null && equipFlag == false)
        {
            rangedData = null;
            rateOfFire = 150;
            bulletVel = 40;
            reloadTime = 200;
            clipSize = 10;


            ammoAmount = 10;
            equipFlag = true;
        }


        //this statement checks to see if the player wants to reload. it checks to make sure the player isnt already reloading
        if (!isReloading && Input.GetButton("Reload") && ammoAmount < clipSize)
        {
            StartCoroutine(Reload());
        }

        //this checks to see if the player wants to shoot. it checks to make sure the player isnt either shooting or reloading already.
        if (!isShooting && !isReloading && Input.GetMouseButton(0))
        {
            StartCoroutine(Shoot(clipSize));
            OnObjectSpawn();
        }
    }

    //basic coroutine for constraints around shooting such as ammo amount and a cooldown timer before the player can shoot again.
    IEnumerator Shoot(int clipSize)
    {
        isShooting = true;  //set the flag

        //if the player has run out of ammo, force them into reloading.
        if (ammoAmount == 0)
        {
            StartCoroutine(Reload());

            isShooting = false;
            yield break; //we break here to avoid the WaitforSeconds that is used to set rate of fire below. otherwise there would be a delay on reloading after the player has already dry fired.
        }
        else          //otherwise simply subtract 1 from the ammo amount each time we fire. 
        {
            ammoAmount--;
        }
        yield return new WaitForSeconds((float)100 / rateOfFire);   //this sets the rate of fire delay

        isShooting = false; //reset the flag
    }

    //this is simply a coroutine that lockss the player out of firing/reloading with a flag, then resets the ammo amount to the
    //clip size of the weapon. it then delays execution to simulate the time taken to relaod. 
    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading... " + reloadTime + " second reload time.");
        ammoAmount = clipSize;
        yield return new WaitForSeconds((float)reloadTime / 100);
        isReloading = false;
    }

    //this is in charge of spawning the bullet game objects
    public void OnObjectSpawn()
    {
        Vector2 firePointPosition = new Vector2(firePoint.transform.position.x, firePoint.transform.position.y);    //get the fire point location

        GameObject bullet = objectPooler.SpawnFromPool("BulletPooler", (firePointPosition), Quaternion.AngleAxis(angle, Vector3.forward));  //get a bullet object from the pooler. and move it to the fire point position.
                                                                                                                                            //using data from above. (direction, angle etc.)
        Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();

        bulletRB.velocity = new Vector2(0, 0);  //set the velocity to 0 for to ensure the bullet doesnt maintain any previous velocity.

        bulletRB.AddForce(direction.normalized * bulletVel, ForceMode2D.Impulse);   //add the new force that actually fires the bullet.
    }

    public void Flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = weaponPart.transform.localScale;
        theScale.y *= -1;
        weaponPart.transform.localScale = theScale;
    }
}
