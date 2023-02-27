using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "My Game/Equipment")]
public class Equipment : InventoryItemData
{
    public EquipmentSlot equipSlot;

    public int armorAdditiveModifier;
    public int armorMultModifier;

    public int damageAdditiveModifier;
    public int damageMultModifier;

    public int healthAdditiveModifier;
    public int healthMultModifier;      //mult modifiers are done on a( 1 + x / 100 ) basis.

    public GameObject buildsPrefab;
    public int buildOffset;

    public int buildTimerAdditiveModifier;  //measured using a x/100 seconds equation
    public int buildTimerMultModifier;

    public int buildCoolDownAdditiveModifier;   //measured using a x/100 seconds equation
    public int buildCoolDownMultModifier;


    public int rateOfFireAdditiveModifier;          //rate of fire is measured using a x/100 seconds equation
    public int rateOfFireMultModifier;

    public int bulletVelocityAdditiveModifier;
    public int bulletVelocityMultModifier;

    public int clipSizeAdditiveModifier;
    public int clipSizeMultModifier;

    public int reloadTimeAdditiveModifier;//measured using a x/100 seconds equation
    public int reloadTimeMultModifier;

    public int meleeDamageAdditiveModifier;
    public int meleeDamageMultModifier;


    public int itemAmount;

    public override void Use()
    {
        base.Use();
        EquipmentManager.instance.Equip(this);
        RemoveFromInventory();
    }
}

public enum EquipmentSlot {Armor, Ranged, Melee, Mobility, Build, Ammo}
