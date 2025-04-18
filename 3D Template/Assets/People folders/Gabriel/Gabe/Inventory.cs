using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<InventoryItem> items = new List<InventoryItem>();
    public int maxSlots = 20;

    public delegate void OnInventoryChanged();
    public event OnInventoryChanged inventoryChangedCallback;

    public GridLayoutGroup inventory;

    public bool AddItem(InventoryItem item, GameObject Object)
    {
        if (items.Count >= maxSlots)
        {
            Debug.Log("Inventory is full!");
            return false;
        }

        InventoryItem existingItem = items.Find(i => i.itemName == item.itemName);
        if (!existingItem.Equals(default(InventoryItem)))
        {
            items[items.IndexOf(existingItem)] = new(existingItem.itemName, existingItem.icon, existingItem.quantity + item.quantity);
        }
        else
        {
            items.Add(item);
        }

        inventoryChangedCallback?.Invoke();
        Destroy(Object);
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
            Debug.LogWarning(item.itemName + " " + item.quantity);
        }
    }
}