using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyObject : MonoBehaviour
{
    public float thrust;
    public float maxSpeed;

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" || collision.tag == "Enemy")
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(0, 0);
            rb.AddForce(new Vector2(0, thrust),ForceMode2D.Impulse);

            if(rb.velocity.y > maxSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, maxSpeed);
            }
        }
    }
}
