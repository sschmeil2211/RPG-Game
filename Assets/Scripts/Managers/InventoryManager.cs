using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    #region Singleton
    public static InventoryManager inventoryManagerInstance;

    private void Awake()
    {
        if (inventoryManagerInstance != null)
        {
            Debug.LogWarning("Mas de un inventario");
            return;
        }
        inventoryManagerInstance = this;
    }
    #endregion

    public int space = 10; //Slots del inventario
    public List<ItemSO> items = new(); //Items actuales del inventario

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    //Agrega un item, si salio bien => true, si salio mal => false
    public bool Add(ItemSO item)
    {
        if (!item.isDefault)
        {
            //Chequeo el espacio
            if(items.Count >= space)
            {
                Debug.Log("No hay espacio suficiente");
                return false;
            }
            items.Add(item);
            onItemChangedCallback?.Invoke();
        }
        return true;
    }

    public void Remove(ItemSO item)
    {
        items.Remove(item);
        onItemChangedCallback?.Invoke();
    }
}
