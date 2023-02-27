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
        EquipmentManager.instance.onEquipmentChanged += onEquipmentChanged;
        charStats = GetComponent<CharacterStats>();
        string healthVal = charStats.currentHealth.ToString();
        text.text = healthVal;
    }

    void onEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        
        if (newItem != null)
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

        if (oldItem != null)
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


        if (currentHealth > baseHealth.GetValue())
        {
            ResetHealth();
            string dialog = charStats.currentHealth.ToString();
            text.text = dialog;
        }
    }

    public override void Die()
    {
        base.Die();
        //kill the player
        PlayerManager.instance.KillPlayer();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            TakeDamage(50);
        }
    }

    public override void TakeDamage(int damage)
    {
        if (invulnerability == false)
        {
            StartCoroutine(Invulnerability(damage));
            
        }
        string dialog = charStats.currentHealth.ToString();
        text.text = dialog;
    }


    public IEnumerator Invulnerability(int damage)
    {
        base.TakeDamage(damage);
        invulnerability = true;
        yield return new WaitForSeconds(1);
        invulnerability = false;
    }



    public override void Heal(int heal)
    {
        base.Heal(heal);
        string dialog = charStats.currentHealth.ToString();
        text.text = dialog;
    }
}
