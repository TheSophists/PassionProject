using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public EnemyData enemyData;
    Transform target;
    public float speed;
    public float nextWaypointDistance = 1f;

    public Transform enemyGFX;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    Seeker seeker;
    Rigidbody2D rb;

    bool start = false;
    public float activateDistance = 38f;
    float currentDistanceX;
    float currentDistanceY;
    bool shoots;
    float maxSpeed = 20f;

    float attackDistance;
    bool flying;
    Vector2 enemyDirection;

    bool movementFlag;

    bool m_FacingRight = true;

    Transform firePoint;
    PlayerManager playerManager;
    GameObject player;
    EnemyAttack enemyAttack;
    EnemyStats enemyStats;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = PlayerManager.instance;
        player = playerManager.player;
        target = player.transform;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        enemyStats = rb.GetComponent<EnemyStats>();


        speed = enemyData.speed;
        attackDistance = enemyData.attackDistance;
        shoots = enemyData.shoots;
        flying = enemyData.flies;

        enemyAttack = GetComponent<EnemyAttack>();

        if (flying)
        {
            InvokeRepeating("UpdatePath", 0f, .5f);
        }

        if (firePoint == null)
        {
            firePoint = transform.Find("FirePoint");
        }

        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void UpdatePath()
    {
        if (seeker != null)
        {
            if (seeker.IsDone())
            {
                seeker.StartPath(rb.position, target.position, OnPathComplete);
            }
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    public void FlyingMovement()
    {
        if (path == null)
        {
            return;
        }
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = speed * Time.deltaTime * direction;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }

    public void GroundedMovement(Vector2 enemyDirection, Rigidbody2D rb)
    {
        bool grounded;

        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position - new Vector3(0f, 1.9f, 0), Vector2.left, 2.5f);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position - new Vector3(0f, 1.9f, 0), Vector2.right, 2.5f);
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position - new Vector3(0f, .5f, 0), Vector2.down, 2.5f);
        if (Mathf.Abs(rb.velocity.x) < maxSpeed)
        {
            rb.AddForce(speed * Time.deltaTime * enemyDirection);
        }

        /*
        Debug.DrawRay(transform.position - new Vector3(0f, 2f), Vector2.left * 2.5f);
        Debug.DrawRay(transform.position - new Vector3(0f, 2f), Vector2.right * 2.5f);
        Debug.DrawRay(transform.position - new Vector3(0f, .5f), Vector2.down * 2.5f);
        */

        if (hitDown.collider != null)
        {
            if (hitDown.collider.CompareTag("Ground"))
            {
                grounded = true;
                if (hitLeft.collider != null && grounded == true)
                {
                    if (hitLeft.collider.CompareTag("Ground"))
                    {
                        rb.constraints = RigidbodyConstraints2D.FreezePosition;
                        rb.constraints = RigidbodyConstraints2D.None;
                        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                        rb.AddForce(Vector2.up * 8F, ForceMode2D.Impulse);
                    }
                }
                else if (hitRight.collider != null && grounded == true)
                {
                    if (hitRight.collider.CompareTag("Ground"))
                    {
                        rb.constraints = RigidbodyConstraints2D.FreezePosition;
                        rb.constraints = RigidbodyConstraints2D.None;
                        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                        rb.AddForce(Vector2.up * 8f, ForceMode2D.Impulse);
                    }
                }

                return;
            }
        }
        grounded = false;
    }


    public void Flip()
    {

        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void Move()
    {
        enemyDirection = (new Vector2(player.transform.position.x, player.transform.position.y) - new Vector2(enemyGFX.position.x, enemyGFX.position.y)).normalized;
        currentDistanceX = Mathf.Abs(target.position.x - enemyGFX.position.x);
        currentDistanceY = Mathf.Abs(target.position.y - enemyGFX.position.y);

        if (enemyDirection.x > 0 && !m_FacingRight)
        {
            Flip();
        }
        else if (enemyDirection.x < 0 && m_FacingRight)
        {
            Flip();
        }

        if (currentDistanceX <= activateDistance && currentDistanceY <= activateDistance)
        {
            start = true;
        }


        //this block is used to make the enemy shoot
        if (currentDistanceX <= attackDistance && currentDistanceY <= attackDistance && shoots == true && enemyAttack.isRunning == false)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
            enemyAttack.StartCoroutine("Shoot");

            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        else if (currentDistanceX <= attackDistance && currentDistanceY <= attackDistance && shoots == true && enemyAttack.isRunning == true)
        {
            movementFlag = false;
        }


        if (start && !flying && movementFlag)
        {
            GroundedMovement(enemyDirection, rb);
        }
        //AI movement, assuming the enemy flies, as grounded enemies will have a different movement system.
        else if (start && flying)
        {
            FlyingMovement();
        }

        movementFlag = true;
    }
}
