using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
    public BoxCollider2D meleeBox;
    PlayerManager playerManager;
    EquipmentManager equipmentManager;
    PlayerStats playerStats;
    int meleeDamage;
    Equipment MeleeData;
    bool flag = false;

    public GameObject weaponPart;
    Vector2 direction;
    float angle;
    float knockback = 10f;

    public GameObject meleeAnimation;


    private void Start()
    {
        playerManager = PlayerManager.instance;
        equipmentManager = EquipmentManager.instance;
        playerStats = playerManager.player.GetComponent<PlayerStats>();
        meleeBox.enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 weaponPointPosition = new Vector2(weaponPart.transform.position.x, weaponPart.transform.position.y);
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        direction = (mousePosition - weaponPointPosition);
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        meleeDamage = playerStats.meleeDamage.GetValue();

        
        if (Input.GetButton("Melee") && flag == false)
        {
            StartCoroutine(MeleeAnimation());
            flag = true;
            StartCoroutine(MeleeOn());
        }

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            EnemyStats enemyStats = collision.GetComponent<EnemyStats>();
            Rigidbody2D enemyRB = collision.GetComponent<Rigidbody2D>();
            enemyRB.AddForce(direction.normalized * knockback, ForceMode2D.Impulse);
            enemyStats.TakeDamage(meleeDamage);
        }
    }

    IEnumerator MeleeOn()
    {
        meleeBox.enabled = true;

        yield return new WaitForSeconds(.1f);

        meleeBox.enabled = false;

        yield return new WaitForSeconds(1f);

        flag = false;
    }

    IEnumerator MeleeOff()
    {

        yield return new WaitForSeconds(.1f);
    }

    IEnumerator MeleeAnimation()
    {
        meleeAnimation.SetActive(true);

        yield return new WaitForSeconds(.1f);

        meleeAnimation.SetActive(false);

    }
}
