using UnityEngine; 

[CreateAssetMenu(fileName = "Equipment", menuName = "Inventory/Equipment")]
public class EquipmentSO : ItemSO//No es un SO como tal, hereda de ItemSO
{
    public EquipmentType equipmentType;
    public int armorModifier;
    public int damageModifier; 
    public SkinnedMeshRenderer meshRenderer;

    public override void Use()
    {
        base.Use();
        EquipmentManager.equipmentManagerInstance.Equip(this);
        RemoveFromInventory();
    }
}
