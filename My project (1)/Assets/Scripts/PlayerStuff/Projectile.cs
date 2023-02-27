using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    GameObject projectile;
    PlayerStats playerStats;
    ObjectPooler objectPooler;

    PlayerManager playerManager;
    bool flag = false; //flag used to make sure collider only hits once

    private void Start()
    {
        playerManager = PlayerManager.instance;
        playerStats = playerManager.player.GetComponent<PlayerStats>();

        objectPooler = ObjectPooler.Instance;
        projectile = this.gameObject;
    }

    private void Update()
    {
        float intX = Mathf.Abs(playerManager.player.transform.position.x - this.gameObject.transform.position.x);
        float intY = Mathf.Abs(playerManager.player.transform.position.y - this.gameObject.transform.position.y);
        if (intX > 100 || intY > 100)
        {

            projectile.SetActive(false);

            if (objectPooler != null)
            {
                objectPooler.poolDictionary["BulletPooler"].Enqueue(projectile);
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        projectile.SetActive(false);

        if (objectPooler != null)
        {
            objectPooler.poolDictionary["BulletPooler"].Enqueue(projectile);
        }

        if (collision.gameObject.GetComponent<EnemyStats>() != null && flag == false)
        {
            flag = true;
            EnemyDamaged(collision.GetComponent<EnemyStats>(), playerStats.damage.GetValue());
        }
        flag = false;   //flag is used to prevent enemies being hit twice
    }

    public void EnemyDamaged(CharacterStats takeDamage, int damage)
    {
        takeDamage.TakeDamage(damage);
    }
}
