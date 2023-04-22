using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EquipmentManager;

public class PlayerStats : CharacterStats
{
    private void Start() => EquipmentManager.equipmentManagerInstance.onEquipmentChanged += OnEquipmentChanged;

    private void OnEquipmentChanged(Equipment newEquip, Equipment oldEquip)
    {
        if(newEquip != null)
        {
            armor.AddModifier(newEquip.armorModifier);
            damage.AddModifier(newEquip.damageModifier);
        }
        if(oldEquip != null)
        {
            armor.RemoveModifier(oldEquip.armorModifier);
            damage.RemoveModifier(oldEquip.damageModifier);
        }
    }
}