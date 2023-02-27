using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDoor : MonoBehaviour
{
    public GameObject door;
    public GameObject enemies;
    private void OnTriggerExit2D(Collider2D collision)
    {

        door.SetActive(true);
        if (enemies != null)
        {
            enemies.SetActive(true);
        }

    }
}
