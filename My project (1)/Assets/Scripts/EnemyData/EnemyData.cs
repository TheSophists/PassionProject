using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "My Game/Enemy Data")]
public class EnemyData : ScriptableObject
{
    //does the enemy fly?
    public bool flies;

    public float speed;

    //the farthest distance between the player and enemy that will cause the enemy to use its attack (0 for melee)
    public float attackDistance;

    //does the enemy shoot?
    public bool shoots;

    //velocity of the projectile if it shoots
    public float bulletVelocity;

    //after the enemy shoots, they may recoil from the shot, this is the integer used to apply that force.
    public int recoilMovement;

    //length of time before the enemy can fire again.
    public int rateOfFire;
}