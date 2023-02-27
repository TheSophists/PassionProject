using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStats : SpecialEnemyStats
{
    public override void Die()
    {
        DungeonManager.instance.newDungeon = false;     //calls the dungeon manager to reset the dungeon for another run
        base.Die();                                     //normal enemy death
    }
}
