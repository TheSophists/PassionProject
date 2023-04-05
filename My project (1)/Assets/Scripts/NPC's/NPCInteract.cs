using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCInteract : MonoBehaviour
{
    public NPCData npcData;         //information about the friendly npc that contains info about the npc such as their potential dialogs
    public Text speech;             //this is the text box that the speech is displayed in
    public GameObject dialogBox;    //the decorative background for the dialog

    bool playerInside = false;      //flag used to check if the player is inside the npc's collider
    bool flag = true;               //this flag is used to prevent the dialog box being opened multiple times on a single input. it is flipped when the interact button is pressed and released.

    int dialogCount = 0;            //counter used to iterate through the array of the dialog. It is currently being stored in an array of strings in the NPC data Scriptable object.

    private void Start()
    {
        dialogBox.SetActive(false); //initiate the dialog box as off.
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")      //if something collides with the npc, check to see if its the player.
        {
            playerInside = true;            //if it is, set this boolean to true, so that if the player presses the interact key while next to the npc, it starts dialog.
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")      //if the player leaves the trigger surrounding the npc
        {
            dialogBox.SetActive(false);     //disable the dialog box (as the player has moved away from the npc)
            playerInside = false;           //player is no longer inside the trigger, so dialog CANNOT be started.
            flag = true;                    //reset the flag that prevents interacting with an npc while a dialog box is open.
        }
    }

    public void Update()
    {
        if (Input.GetButtonDown("Interact") && playerInside == true && flag == true)        //if the player wants to interact, is inside the npc's trigger, and the interact button hasnt already been pressed.
        {
            flag = false;                   //flip the input check flag
            dialogBox.SetActive(true);      //turn on the dialog box UI element


            if (dialogCount < npcData.dialog.Length)        //if our counter is less than the length of the dialog array
            {
                Talk(dialogCount);                          //start "Talking", with the appropriate array element (given by dialogCount)
            }
            else
            {
                StopTalk();                                 //if the npc has reached the end of its dialog. 
            }
        }
        else if (Input.GetButtonUp("Interact") && playerInside == true && flag == false)    //this statement prevents multiple inputs over multiple frames when the player only intends for it to happen instantly
        {                                                                                   //before, you could cycle through multiple dialog options with a single button press
            flag = true;                                                                    //but, since we wait for the button to be released before we flip the flag, we cant continue dialog until the button has
        }                                                                                   //been pressed, released, and then pressed again.
    }

    public void Talk(int counter)               //this funtion handles which element of the dialog array should be displayed.
    {
        speech.text = npcData.dialog[counter];  //the text box's text is set to whatever is the npc Data dialog array at the [counter] element.
        dialogCount += 1;                       //increase the counter to display the next option in the array the next time that Talk() is called. 
    }

    public void StopTalk()                      //this function removes the UI for dialog 
    {
        dialogBox.SetActive(false);     //turn the dialog box off
        dialogCount = 0;                //reset the counter 
    }
}
