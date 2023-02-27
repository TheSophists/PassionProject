using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEnemyStats : EnemyStats
{
    public GameObject door;     //the door that killing this enemy will open.
    public override void Die()
    {
        door.SetActive(false);  //turn the door off.
        base.Die();             //kill the enemy.
    }
}
