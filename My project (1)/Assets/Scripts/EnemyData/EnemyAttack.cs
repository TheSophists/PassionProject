using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    PlayerManager playermanager;
    Rigidbody2D player;
    Rigidbody2D enemy;
    PlayerStats playerStats;
    EnemyStats enemyStats;
    int direction;
    public EnemyData enemyData;
    EnemyProjectile enemyProjectile;
    public GameObject bulletPrefab;
    int recoilMovement;
    int rateOfFire;
    Transform firePoint;
    ObjectPooler objectPooler;
    Vector2 shootDirection;
    public bool isRunning = false;

    public void Start()
    {
        playermanager = PlayerManager.instance;     //finds the player
        enemyStats = GetComponent<EnemyStats>();    //gets the enemy Stats
        playerStats = playermanager.player.GetComponent<PlayerStats>();
        player = playermanager.player.GetComponent<Rigidbody2D>();
        enemy = this.GetComponent<Rigidbody2D>();

        enemyProjectile = bulletPrefab.GetComponent<EnemyProjectile>();

        objectPooler = ObjectPooler.Instance;

        if (enemyData != null)
        {
            recoilMovement = enemyData.recoilMovement;
            rateOfFire = enemyData.rateOfFire;
        }

        if (firePoint == null)
        {
            firePoint = transform.Find("FirePoint");
        }
    }



    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.transform.position.x - this.transform.position.x < 0)
            {
                direction = -1;
            }
            else
            {
                direction = 1;
            }

            playerStats = collision.gameObject.GetComponent<PlayerStats>();
            enemyStats = this.GetComponent<EnemyStats>();

            StartCoroutine(EnemyFreeze());


            playerStats.TakeDamage(enemyStats.damage.GetValue());
        }
    }

    public IEnumerator EnemyFreeze()
    {
        if (playerStats.invulnerability == false)
        {
            //this is the knockback force on the player;
            enemy.constraints = RigidbodyConstraints2D.FreezePosition;
            player.AddForce(new Vector2(direction * 150, 10), ForceMode2D.Impulse);
            yield return new WaitForSeconds(1);
            enemy.constraints = RigidbodyConstraints2D.None;
            enemy.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    public IEnumerator Shoot()  //called from EnemyAI
    {
        isRunning = true;       //prevents shooting before their rof allows

        Vector2 playerPosition = new Vector2(player.transform.position.x, player.transform.position.y);
        Vector2 weaponPosition = new Vector2(firePoint.position.x, firePoint.position.y);
        shootDirection = (playerPosition - weaponPosition).normalized;
        float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
        OnObjectSpawn(angle);



        enemy.constraints = RigidbodyConstraints2D.None;
        if (recoilMovement != 0)
        {
            RecoilMovement();
        }
        yield return new WaitForSeconds(rateOfFire);

        isRunning = false;
    }

    public void OnObjectSpawn(float angle)
    {
        Vector2 firePointPosition = new Vector2(firePoint.transform.position.x, firePoint.transform.position.y);

        GameObject bullet = objectPooler.SpawnFromPool("EnemyBulletPooler", (firePointPosition), Quaternion.AngleAxis(angle, Vector3.forward));
        Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();

        bullet.GetComponent<EnemyProjectile>().damage = enemyStats.damage.GetValue();       //sets damage value to the specific projectile that spawned.

        bulletRB.velocity = new Vector2(0, 0);

        bulletRB.AddForce(shootDirection.normalized * enemyData.bulletVelocity, ForceMode2D.Impulse);
    }

    public void RecoilMovement()
    {

        enemy.AddForce(transform.up * recoilMovement, ForceMode2D.Impulse); //need too add this movement upward into enemy data

    }
}
