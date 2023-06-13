using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    private InventoryManager _inventoryManagerInstance;
    private List<InventorySlot> _inventorySlots;

    public GameObject inventoryUI;
    public Transform itemsParent;

    private void Start()
    {
        _inventoryManagerInstance = InventoryManager.inventoryManagerInstance;
        _inventoryManagerInstance.onItemChangedCallback += UpdateUI;
        _inventorySlots = itemsParent.GetComponentsInChildren<InventorySlot>().ToList();
    }

    private void Update()
    {
        if(Input.GetButtonDown("Inventory"))
            inventoryUI.SetActive(!inventoryUI.activeSelf);
    }

    private void UpdateUI()
    {
        Debug.Log("Actualziando UI");
        for(int i = 0; i < _inventorySlots.Count; i++)
        {
            if(i < _inventoryManagerInstance.items.Count)
                _inventorySlots[i].AddItem(_inventoryManagerInstance.items[i]);
            else
                _inventorySlots[i].DeleteItem();
        }
    }
}
