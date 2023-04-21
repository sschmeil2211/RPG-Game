using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item")]
public class ItemSO : ScriptableObject
{
    public bool isDefaultItem = false;
    public Sprite itemIcon;
    public string itemName;

    public virtual void Use()
    {
        Debug.Log("Se uso " + name);
    }

    public void RemoveFromInventory()
    {
        InventoryManager.inventoryManagerInstance.Remove(this);
    }
}
