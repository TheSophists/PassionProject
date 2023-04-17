using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "My Game/Equipment")]
public class Equipment : InventoryItemData
{

    public EquipmentSlot equipSlot;


    //mult modifiers are done on a( 1 + x / 100 ) basis.

    public int armorAdditiveModifier;       //armor is a value that is subtracted from the damage the character is taking. 7 damage attacker, 3 armor on defender, 4 damage dealt
    public int armorMultModifier;           

    public int damageAdditiveModifier;      //increases damage dealt
    public int damageMultModifier;

    public int healthAdditiveModifier;      //changes health. Base health stays the same, this simply modifies that value to give us current health
    public int healthMultModifier;          //NOT FUNCTIONAL

    public GameObject buildsPrefab;         //offset needed for specific builds to show up in the proper world space location
    public int buildOffset;

    public int buildTimerAdditiveModifier;  //measured using a x/100 seconds equation. the amount of time that a build appears in the game world before disappearing
    public int buildTimerMultModifier;

    public int buildCoolDownAdditiveModifier;   //measured using a x/100 seconds equation. The amount of time that must pass after building, before building again.
    public int buildCoolDownMultModifier;


    public int rateOfFireAdditiveModifier;          //rate of fire is measured using a 100/x seconds equation. this means, the higher the value, the shorter the wait time.
    public int rateOfFireMultModifier;              //this is how frequently the player can shoot

    public int bulletVelocityAdditiveModifier;      //adds to the current bullet velocity value
    public int bulletVelocityMultModifier;

    public int clipSizeAdditiveModifier;            //adds to the size of the players magazine. More bullets before reloading
    public int clipSizeMultModifier;

    public int reloadTimeAdditiveModifier;          //measured using a x/100 seconds equation
    public int reloadTimeMultModifier;

    public int meleeDamageAdditiveModifier;         //adds damage points to the melee attacks.
    public int meleeDamageMultModifier;


    public int itemAmount;                          //the quantity of items in the stack

    public override void Use()      //intended to be overwritten
    {
        base.Use();
        EquipmentManager.instance.Equip(this);  
        RemoveFromInventory();
    }
}

public enum EquipmentSlot {Armor, Ranged, Melee, Mobility, Build, Ammo}     //these are the "slots" that equipment is stored in, based on the type of equipment that it is.
