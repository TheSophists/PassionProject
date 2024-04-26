using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyObject : MonoBehaviour
{
    public float thrust;
    public float maxSpeed;

    public Animator animator;
    public CharacterController2D characterController;



    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.tag == "Player" || collision.tag == "Enemy")       //if the objects colliding with the bouncy object is a player or an enemy.
        {
            animator.SetTrigger("Player Enter");
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();     //get the Rigid body attached to the "character" object
            rb.velocity = new Vector2(0, 0);                            //set their velocity to 0 (this makes the bouncy object work consistently, regardless of the character's velocity beforehand.
            rb.AddForce(new Vector2(0, thrust),ForceMode2D.Impulse);    //add the force (based on thrust set in the Bouncy Object Data script

            if(rb.velocity.y > maxSpeed)                                //if the character's velocity is going above its max velocity in the y direction
            {
                rb.velocity = new Vector2(rb.velocity.x, maxSpeed);     //set its y velocity to its max (this prevents it from exceeding max velocity)
            }
        }
    }
}
