using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerActivate : MonoBehaviour
{
    bool start;
    EnemyAI enemyAI; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        List<GameObject> children = new List<GameObject>();

        foreach (Transform child in transform)
        {
            if (child.tag == "Enemy")
            {
                children.Add(child.gameObject);
            }
        }

        foreach (GameObject enemy in children)
        {
            enemyAI = enemy.GetComponent<EnemyAI>();
            enemyAI.start = true;
        }
    }
}
