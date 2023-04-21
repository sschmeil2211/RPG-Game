using UnityEngine;

public enum EquipmentSlot
{
    HEAD,
    CHEST,
    LEGS,
    WEAPON,
    SHIELD
}

[CreateAssetMenu(fileName = "Equipment", menuName = "Inventory/Equipment")]
public class Equipment : ItemSO
{
    public EquipmentSlot equipSlot;
    public int armorModifier;
    public int damageModifier; 

    public override void Use()
    {
        base.Use();
        EquipmentManager.equipmentManagerInstance.Equip(this);
        RemoveFromInventory();
    }
}
