using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    private ItemSO _item;

    public Button removeButton;
    public Image icon;

    public void AddItem(ItemSO newItem)
    {
        _item = newItem;
        icon.sprite = _item.itemIcon;
        icon.enabled = true;
        removeButton.interactable = true;
    }

    public void DeleteItem()
    {
        _item = null;
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    public void OnRemoveButton() => InventoryManager.inventoryManagerInstance.Remove(_item);

    public void UseItem()
    {
        if (_item == null)
            return;
        _item.Use();
    }
}
