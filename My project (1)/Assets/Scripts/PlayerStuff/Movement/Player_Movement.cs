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

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (horizontalMove > 0)         //used to find last direction faced
        {
            playerDirection = true;
        }
        else if (horizontalMove < 0)
        {
            playerDirection = false;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        if (Input.GetKey(KeyCode.LeftShift) && crouch == false)
        {
            runSpeed = 120f;
        }
        else
        {
            runSpeed = 60f;
        }

        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }
    }

    private void FixedUpdate()
    {
        //move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }
}
