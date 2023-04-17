using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : CharacterStats
{
    public Stat buildCoolDown;
    public Stat meleeDamage;
    public Stat buildTimer;
    public Stat rateOfFire;
    public Stat bulletVelocity;
    public Stat clipSize;
    public Stat reloadTime;

    CharacterStats charStats;
    public Text text;
    public bool invulnerability = false;

    // Start is called before the first frame update
    void Start()
    {
        EquipmentManager.instance.onEquipmentChanged += onEquipmentChanged;     //adds an event ?listener? for changing equipment
        charStats = GetComponent<CharacterStats>();
        string healthVal = charStats.currentHealth.ToString();                  //store the players health value in a string
        text.text = healthVal;                                                  //text box that displays player health string
    }

    void onEquipmentChanged(Equipment newItem, Equipment oldItem)       //when the equipment changed event is invoked. (commonly called from Equipment Manager)
    {

        if (newItem != null)        //if the player is equipping a new item
        {
            baseHealth.AddAdditiveModifier(newItem.healthAdditiveModifier);
            baseHealth.AddMultModifier(newItem.healthMultModifier);

            armor.AddAdditiveModifier(newItem.armorAdditiveModifier);
            armor.AddMultModifier(newItem.armorMultModifier);

            damage.AddAdditiveModifier(newItem.damageAdditiveModifier);
            damage.AddMultModifier(newItem.damageMultModifier);

            buildCoolDown.AddAdditiveModifier(newItem.buildCoolDownAdditiveModifier);
            buildCoolDown.AddMultModifier(newItem.buildCoolDownMultModifier);

            buildTimer.AddAdditiveModifier(newItem.buildTimerAdditiveModifier);
            buildTimer.AddMultModifier(newItem.buildTimerMultModifier);

            rateOfFire.AddAdditiveModifier(newItem.rateOfFireAdditiveModifier);
            rateOfFire.AddMultModifier(newItem.rateOfFireMultModifier);

            bulletVelocity.AddAdditiveModifier(newItem.bulletVelocityAdditiveModifier);
            bulletVelocity.AddMultModifier(newItem.bulletVelocityMultModifier);

            clipSize.AddAdditiveModifier(newItem.clipSizeAdditiveModifier);
            clipSize.AddMultModifier(newItem.clipSizeMultModifier);

            reloadTime.AddAdditiveModifier(newItem.reloadTimeAdditiveModifier);
            reloadTime.AddMultModifier(newItem.reloadTimeMultModifier);

            meleeDamage.AddAdditiveModifier(newItem.meleeDamageAdditiveModifier);
            meleeDamage.AddMultModifier(newItem.meleeDamageMultModifier);
        }

        if (oldItem != null)                //if the player is removing equipment
        {
            baseHealth.RemoveAdditiveModifier(oldItem.healthAdditiveModifier);
            baseHealth.RemoveMultModifier(oldItem.healthMultModifier);

            armor.RemoveAdditiveModifier(oldItem.armorAdditiveModifier);
            armor.RemoveMultModifier(oldItem.armorMultModifier);

            damage.RemoveAdditiveModifier(oldItem.damageAdditiveModifier);
            damage.RemoveMultModifier(oldItem.damageMultModifier);

            buildCoolDown.RemoveAdditiveModifier(oldItem.buildCoolDownAdditiveModifier);
            buildCoolDown.RemoveMultModifier(oldItem.buildCoolDownMultModifier);

            buildTimer.RemoveAdditiveModifier(oldItem.buildTimerAdditiveModifier);
            buildTimer.RemoveMultModifier(oldItem.buildTimerMultModifier);

            rateOfFire.RemoveAdditiveModifier(oldItem.rateOfFireAdditiveModifier);
            rateOfFire.RemoveMultModifier(oldItem.rateOfFireMultModifier);

            bulletVelocity.RemoveAdditiveModifier(oldItem.bulletVelocityAdditiveModifier);
            bulletVelocity.RemoveMultModifier(oldItem.bulletVelocityMultModifier);

            clipSize.RemoveAdditiveModifier(oldItem.clipSizeAdditiveModifier);
            clipSize.RemoveMultModifier(oldItem.clipSizeMultModifier);

            reloadTime.RemoveAdditiveModifier(oldItem.reloadTimeAdditiveModifier);
            reloadTime.RemoveMultModifier(oldItem.reloadTimeMultModifier);

            meleeDamage.RemoveAdditiveModifier(oldItem.meleeDamageMultModifier);
            meleeDamage.RemoveMultModifier(oldItem.meleeDamageMultModifier);
        }
    }

    public override void Die()      //this is incomplete, it handles what happens to the player after death.
    {
        base.Die();
        //kill the player
        //*********this will probably need to reset any dungeon instances, depending on how i reload the scene whenthe player dies******
        PlayerManager.instance.KillPlayer();
    }

    public override void TakeDamage(int damage)     //overriding the takeDamage function of CharacterStats in order to add invulnerability to the player.
    {
        Debug.Log("Invuln: " + invulnerability);
        if (invulnerability == false)                           //if the player is not invulnerable
        {
            base.TakeDamage(damage);                            //call the base of TakeDamage, which handles health changes and damage dealt
            invulnerability = true;                             //as players have taken damage, theey are now invulnerable
            StartCoroutine(Invulnerability());                  //start the invul coroutine
        }

        DisplayHealth();
    }


    public IEnumerator Invulnerability()
    {
        yield return new WaitForSeconds(1);     //delay removing the invuln
        invulnerability = false;                //reset invuln
    }



    public override void Heal(int heal)         //overrides CharacterStats Heal() in order to update the 
    {
        base.Heal(heal);
        DisplayHealth();
    }

    public void DisplayHealth()                                 //displays the players health in the appropriate box
    {
        string dialog = charStats.currentHealth.ToString();     //store the health stat in a string
        text.text = dialog;                                     //update the text box's text
    }
}
