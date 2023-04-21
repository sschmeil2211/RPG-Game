using UnityEngine;

public class ItemPickUp : Interactable
{
    public ItemSO item;

    private void PickUp()
    {
        Debug.Log("Item levantado: " + item.name);
        bool pickedUpSuccessfully = InventoryManager.inventoryManagerInstance.Add(item);
        if (pickedUpSuccessfully)
            Destroy(gameObject);
    }

    public override void Interact()
    {
        base.Interact();
        PickUp();
    }
}
