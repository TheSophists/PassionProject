using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public CharacterController2D controller;

    public float runSpeed = 60f;

    public bool playerDirection;

    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;


    //most of the values set here are used to control the if statements in the character controller2D script. When the boolean values are flipped here based on
    //player input, certain if loops (that handle jumping for example) will start once the keys are presssed in this script.
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed; //get the input from the arrow keys and multiply by the runspeed.


        //if the player has pressed the jump button.
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        //if the player presses the sprint key, while the crouch key is not pressed.
        if (Input.GetKey(KeyCode.LeftShift) && crouch == false)
        {
            runSpeed = 120f;    //set the run speed to the "sprinting" speed.
        }
        else
        {
            runSpeed = 60f;     //otherwise, usethe default speed.
        }

        //if the crouch button is down
        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;      //start crouching
        }
        //when the crouch button has been released.
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false; //stop crouching
        }
    }

    private void FixedUpdate()
    {
        //move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);    //call the Move function in the character controller 2D script using the boolean values set in the script 
        jump = false;       //reset the jump boolean.
    }
}
