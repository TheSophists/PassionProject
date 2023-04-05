using UnityEngine;

public class CharacterStats : MonoBehaviour     //this class is a generic function designed to handle the stats of all character types. Bosses, players, npc's
{
    public Stat baseHealth;
    public int currentHealth { get; private set; }
    public Stat damage;
    public Stat armor;

    void Awake()
    {
        currentHealth = baseHealth.GetValue();              //get the base health of the character. This value is set in the inspector
    }

    public virtual void TakeDamage(int damage)              //function for dealing damage to players or npc's
    {
        damage -= armor.GetValue();                         //subtract the armor value of the defender from the damage of the attacker. 
        damage = Mathf.Clamp(damage, 0, int.MaxValue);      //keeps the damage value betweem 0 and int.MaxValue, this prevents negative damage from armor (which would heal)

        currentHealth -= damage;                            //subtract the damage amount from 

        Debug.Log(transform.name + " takes " + damage + " damage. It now has " + currentHealth + " health and " + armor.GetValue() + " armor");

        if (currentHealth <= 0)     //if the character's health drops to or below 0, the character dies.
        {
            Die();
        }
    }

    public virtual void Heal(int heal)                          //this will typically be called when the player uses an inventory item to heal.
    {
        if(baseHealth.GetValue() - currentHealth <= heal)       //if the basehealth - current health value is less than or equal to the healing value, 
        {
            currentHealth = baseHealth.GetValue();              //then set the health to the character's base health. This prevents healing above the base health value.
        }
        else
        {
            currentHealth += heal;                              //else, simply add the heal value to the characters current health.
        }
    }

    public virtual void Die()
    {
        //This method is intended to be overwritten (by enemies and players who have differing death mechanics, this will be overridden in the children of this script, namely PlayerStats, EnemyStats, Boss Stats, etc.)

    }
}
