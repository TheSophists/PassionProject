using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyGFX : MonoBehaviour
{
    public AIPath aiPath;

    // Update is called once per frame
    void FixedUpdate()
    {
        //these flip the Visuals of the enemy based on its velocity.
        if (aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (aiPath.desiredVelocity.x <= -0.0f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
