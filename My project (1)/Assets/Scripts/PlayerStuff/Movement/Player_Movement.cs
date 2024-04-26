using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public CharacterController2D controller;

    float runSpeed = 60f;

    public bool playerDirection;

    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;

    PlayerManager playerManager;
    PlayerStats playerStats;
    Rigidbody2D playerRb;


    public void Start()
    {
        playerManager = PlayerManager.instance;
        playerRb = playerManager.player.GetComponent<Rigidbody2D>();
        playerStats = playerManager.player.GetComponent<PlayerStats>();
    }


    //most of the values set here are used to control the if statements in the character controller2D script. When the boolean values are flipped here based on
    //player input, certain if loops (that handle jumping for example) will start once the keys are presssed in this script.
    void Update()
    {
        Move();
    }

    public void Move()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed; //get the input from the arrow keys and multiply by the runspeed.


        //if the player has pressed the jump button.
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
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
