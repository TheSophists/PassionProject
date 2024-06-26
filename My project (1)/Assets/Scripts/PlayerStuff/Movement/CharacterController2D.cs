using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private float m_JumpForce = 300f;                          // Amount of force added when the player jumps.
    [Range(0, 1)][SerializeField] private float m_CrouchSpeed = .6f;          // Amount of maxSpeed applied to crouching movement. 1 = 100%
    [Range(0, .3f)][SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
    [SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
    [SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings
    [SerializeField] private Collider2D m_CrouchDisableCollider;                // A collider that will be disabled when crouching
    [SerializeField] private Transform weaponPart;

    const float k_GroundedRadius = .95f; // Radius of the overlap circle to determine if grounded
    private bool m_Grounded;            // Whether or not the player is grounded.
    const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
    private Rigidbody2D m_Rigidbody2D;
    public bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private Vector3 m_Velocity = Vector3.zero;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;      //an event that is trigger when the player lands back on the ground.

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    public BoolEvent OnCrouchEvent;
    private bool m_wasCrouching = false;

    static bool jumped;


    private void Awake()
    {
        //OnLandEvent.AddListener(Testing); //listener being kept as an example on how to use listeners/delegates.

        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)            //sets the event types as theyre null on start.
            OnLandEvent = new UnityEvent();

        if (OnCrouchEvent == null)
            OnCrouchEvent = new BoolEvent();
    }

    private void FixedUpdate()
    {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)      //checks all colliders and ignores collisions with the player object. It only checks for "ground" objects.
            {
                m_Grounded = true;      //we know the player has hit an object other than itself, so make it grounded.
                if (!wasGrounded)
                {
                    OnLandEvent.Invoke();   //if the player was not grounded before, then trigger a land event. 
                }
            }
        }
    }


    public void Move(float move, bool crouch, bool jump)
    {
        Vector2 targetVelocity = new Vector2(0,0);
        // If crouching, check to see if the character can stand up
        if (!crouch)
        {
            // If the character has a ceiling preventing them from standing up, keep them crouching
            if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
            {
                crouch = true;
            }
        }

        //only control the player if grounded or airControl is turned on
        if (m_Grounded || m_AirControl)
        {
            // If crouching and grounded
            if (crouch && m_Grounded)
            {
                if (!m_wasCrouching)
                {
                    m_wasCrouching = true;
                    OnCrouchEvent.Invoke(true);
                }

                // Reduce the speed by the crouchSpeed multiplier
                move *= m_CrouchSpeed;

                // Disable one of the colliders when crouching
                if (m_CrouchDisableCollider != null)
                    m_CrouchDisableCollider.enabled = false;
            }
            else
            {
                // Enable the collider when not crouching
                if (m_CrouchDisableCollider != null)
                    m_CrouchDisableCollider.enabled = true;

                if (m_wasCrouching)
                {
                    m_wasCrouching = false;
                    OnCrouchEvent.Invoke(false);
                }
            }

            // Move the character by finding the target velocity
            targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);

            // And then smoothing it out and applying it to the character
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

            // If the input is moving the player right and the player is facing left...
            if (move > 0 && !m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
        }
        // If the player should jump...
        if (m_Grounded && jump)
        {
            // Add a vertical force to the player.
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0);
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
        }

        if (move == 0 && m_Grounded == true && jump == false)
        {
            Vector2 currentVelocity = m_Rigidbody2D.velocity;
            currentVelocity.x = 0;
            m_Rigidbody2D.velocity = currentVelocity;
            m_Rigidbody2D.gravityScale = 0;
        }
        else if (m_Grounded == false && jumped == false)
        {
            m_Rigidbody2D.gravityScale = 12;
        }
        else
        {
            m_Rigidbody2D.gravityScale = 12;
        }

        if (m_Rigidbody2D.velocity.x != 0 && Mathf.Abs(m_Rigidbody2D.velocity.x) < Mathf.Abs(targetVelocity.x)) //used to set a minimum speed
        {
            targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
            // And then smoothing it out and applying it to the character
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
        }
        if (m_Rigidbody2D.velocity.x != 0 && Mathf.Abs(m_Rigidbody2D.velocity.x) > Mathf.Abs(targetVelocity.x)) //used to set a max speed.
        {
            targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
            // And then smoothing it out and applying it to the character
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
        }
    }


    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        weaponPart.transform.localScale = theScale;
        transform.localScale = theScale;
    }

    public void FlipJumped()
    {
        jumped = true;
    }

    /*public void Testing()
    {
        Debug.Log("OnLandEvent Triggered Here and Stuff for visibility.");
    }*/
    //this is being left in as an example of how i would use a listener for an OnLandEvent.
}