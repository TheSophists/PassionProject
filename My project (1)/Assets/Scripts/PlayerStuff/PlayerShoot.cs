using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class PlayerShoot : MonoBehaviour, IPooledObject
{
    public GameObject projectile;
    public GameObject player;
    public Camera playerCam;
    PlayerStats myStats;

    Transform weaponPart;
    Transform firePoint;

    Equipment rangedData;
    int rateOfFire;
    int bulletVel;            //this value will be used to set bullet velocity.
    EquipmentManager equipManager; //instance of the current equipment

    bool isRunning = false;

    int ammoAmount;
    int clipSize;
    int reloadTime;

    ObjectPooler objectPooler;

    Vector2 direction;
    float angle;


    private void Start()
    {
        equipManager = EquipmentManager.instance;
        objectPooler = ObjectPooler.Instance;
        myStats = player.GetComponent<PlayerStats>();

        if (firePoint == null)
        {
            weaponPart = transform.Find("WeaponPart");

            firePoint = weaponPart.transform.Find("FirePoint");
        }
    }

    void FixedUpdate()
    {
        Vector2 weaponPointPosition = new Vector2(weaponPart.transform.position.x, weaponPart.transform.position.y);
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        direction = (mousePosition - weaponPointPosition);
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        weaponPart.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        if (equipManager.GetRanged() != null && rangedData != equipManager.GetRanged())
        {
            rangedData = equipManager.GetRanged();
            rateOfFire = myStats.rateOfFire.GetValue();
            bulletVel = myStats.bulletVelocity.GetValue();
            reloadTime = myStats.reloadTime.GetValue();
            clipSize = myStats.clipSize.GetValue(); ;


            ammoAmount = clipSize;
        }
        else if (equipManager.GetRanged() == null)
        {
            rangedData = null;
            rateOfFire = 150;
            bulletVel = 40;
            reloadTime = 200;
            clipSize = 10;


            ammoAmount = 10;
        }



        if (!isRunning && Input.GetButton("Reload") && ammoAmount < clipSize)
        {
            StartCoroutine(Reload());
        }

        if (!isRunning && Input.GetMouseButton(0))
        {
            StartCoroutine(Shoot(clipSize));
            OnObjectSpawn();
        }
    }

    IEnumerator Shoot(int clipSize)
    {
        isRunning = true;
        if (ammoAmount == 1)
        {
            StartCoroutine(Reload());

            yield break;
        }
        else
        {
            ammoAmount--;
        }
        yield return new WaitForSeconds((float)rateOfFire / 100);

        isRunning = false;
    }

    IEnumerator Reload()
    {
        isRunning = true;
        Debug.Log("Reloading... " + reloadTime + " second reload time.");
        ammoAmount = clipSize;
        yield return new WaitForSeconds((float)reloadTime / 100);
        isRunning = false;
    }

    public void OnObjectSpawn()
    {
        Vector2 firePointPosition = new Vector2(firePoint.transform.position.x, firePoint.transform.position.y);

        GameObject bullet = objectPooler.SpawnFromPool("BulletPooler", (firePointPosition), Quaternion.AngleAxis(angle, Vector3.forward));
        Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();

        bulletRB.velocity = new Vector2(0, 0);

        bulletRB.AddForce(direction.normalized * bulletVel, ForceMode2D.Impulse);
    }
}
