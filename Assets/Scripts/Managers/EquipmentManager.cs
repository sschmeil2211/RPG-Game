using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton
    public static EquipmentManager equipmentManagerInstance;

    private void Awake() => equipmentManagerInstance = this; 
    #endregion

    private List<EquipmentSO> _equipments;  //Items del tipo equipo que tenemos equipados
    private List<SkinnedMeshRenderer> _currentSkinnedMeshes;  
    private InventoryManager _inventoryManager;

    public delegate void OnEquipmentChanged(EquipmentSO newEquip, EquipmentSO oldEquip);
    public OnEquipmentChanged onEquipmentChanged;
    public SkinnedMeshRenderer targetMesh;

    private void Start()
    {
        _inventoryManager = InventoryManager.inventoryManagerInstance;
        //Inicializo el equipamiento actual con el numero de slots
        int slots = System.Enum.GetNames(typeof(EquipmentType)).Length; 
        _equipments = new();
        _currentSkinnedMeshes = new();
        for (int i = 0; i < slots; i++)
        {
            _equipments.Add(null);
            _currentSkinnedMeshes.Add(null);
        }
        print(_equipments.Count);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.U))
            UnequipAll();
    }

    public void Equip(EquipmentSO newEquip)
    {
        //Busco la ranura en la que debe ir el equipo
        int index = (int)newEquip.equipmentType;
        EquipmentSO oldEquip = null;
        if (_equipments[index] != null)
        {
            oldEquip = _equipments[index];
            _inventoryManager.Add(oldEquip);
        } 
        onEquipmentChanged?.Invoke(newEquip, oldEquip);//Elemento equipado, activo el callback
        _equipments[index] = newEquip;
        SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(newEquip.meshRenderer);
        newMesh.transform.parent = targetMesh.transform;
        newMesh.bones = targetMesh.bones;
        newMesh.rootBone = targetMesh.rootBone;
        _currentSkinnedMeshes[index] = newMesh;
    }

    public void Unequip(int index)
    {
        //Solo desequipo si el slot de equipo se esta usando
        if (_equipments[index] != null)
        {
            if (_currentSkinnedMeshes[index].gameObject)
                Destroy(_currentSkinnedMeshes[index].gameObject);
            EquipmentSO oldEquip = _equipments[index];
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
