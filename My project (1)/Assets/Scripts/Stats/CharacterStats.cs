using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public Stat baseHealth;
    public int currentHealth { get; private set; }
    public Stat damage;
    public Stat armor;

    void Awake()
    {
        currentHealth = baseHealth.GetValue();
    }

    public virtual void TakeDamage(int damage)
    {
        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currentHealth -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage. It now has " + currentHealth + " health and " + armor.GetValue() + " armor");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Heal(int heal)
    {
        if(baseHealth.GetValue() - currentHealth <= heal)
        {
            currentHealth = baseHealth.GetValue();
        }
        else
        {
            currentHealth += heal;
        }
    }

    public virtual void Die()
    {
        //This method is intended to be overwritten (by enemies and players who have differing death mechanics)

    }

    public void ResetHealth()
    {
        currentHealth = baseHealth.GetValue();
        Debug.Log(currentHealth);
    }
}
