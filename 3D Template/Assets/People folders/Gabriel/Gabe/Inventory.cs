using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<InventoryItem> items = new List<InventoryItem>();
    public int maxSlots = 20;

    public delegate void OnInventoryChanged();
    public event OnInventoryChanged inventoryChangedCallback;

    public bool AddItem(InventoryItem item)
    {
        if (items.Count >= maxSlots)
        {
            Debug.Log("Inventory is full!");
            return false;
        }

        InventoryItem existingItem = items.Find(i => i.itemName == item.itemName);
        if (existingItem != null)
        {
            existingItem.quantity += item.quantity;
        }
        else
        {
            items.Add(item);
        }

        inventoryChangedCallback?.Invoke();
        return true;
    }

    public void RemoveItem(InventoryItem item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            inventoryChangedCallback?.Invoke();
        }
    }
    private void Update()
    {
        foreach (var item in items)
        {
            print(item.itemName);
        }
    }
}