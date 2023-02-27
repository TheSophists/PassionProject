using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCInteract : MonoBehaviour
{
    public NPCData npcData;
    public Text speech;
    public GameObject dialogBox;

    bool playerInside = false;
    bool flag = true;

    int dialogCount = 0;

    private void Start()
    {
        dialogBox.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerInside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            dialogBox.SetActive(false);
            playerInside = false;
            flag = true;
        }
    }

    public void Update()
    {
        if (Input.GetButtonDown("Interact") && playerInside == true && flag == true)
        {
            flag = false;
            dialogBox.SetActive(true);


            if (dialogCount < npcData.dialog.Length)
            {
                Talk(dialogCount);
            }
            else
            {
                StopTalk();
            }
        }
        else if (Input.GetButtonUp("Interact") && playerInside == true && flag == false)
        {
            flag = true;
        }
    }

    public void Talk(int counter)
    {
        speech.text = npcData.dialog[counter];
        dialogCount += 1;
    }

    public void StopTalk()
    {
        dialogBox.SetActive(false);
        dialogCount = 0;
    }
}
