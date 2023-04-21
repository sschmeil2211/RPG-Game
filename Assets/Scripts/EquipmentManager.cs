using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton
    public static EquipmentManager equipmentManagerInstance;

    private void Awake() => equipmentManagerInstance = this; 
    #endregion

    private List<Equipment> _equipments;  
    private InventoryManager _inventoryManager;

    public delegate void OnEquipmentChanged(Equipment newEquip, Equipment oldEquip);
    public OnEquipmentChanged onEquipmentChanged;

    private void Start()
    {
        _inventoryManager = InventoryManager.inventoryManagerInstance;
        int slots = System.Enum.GetNames(typeof(EquipmentSlot)).Length; 
        _equipments = new();
        for (int i = 0; i < slots; i++)
            _equipments.Add(null);
        print(_equipments.Count);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.U))
            UnequipAll();
    }

    public void Equip(Equipment newEquip)
    {
        int index = (int)newEquip.equipSlot;
        Equipment oldEquip = null;
        if (_equipments[index] != null)
        {
            oldEquip = _equipments[index];
            _inventoryManager.Add(oldEquip);
        } 
        onEquipmentChanged?.Invoke(newEquip, oldEquip);
        _equipments[index] = newEquip;
    }

    public void Unequip(int index)
    {
        if (_equipments[index] != null)
        {
            Equipment oldEquip = _equipments[index];
            _inventoryManager.Add(oldEquip);
            _equipments[index] = null; 
            onEquipmentChanged?.Invoke(null, oldEquip);
        }
    }

    public void UnequipAll() 
    {
        for(int i = 0; i < _equipments.Count; i++)
            Unequip(i);
    }
}
