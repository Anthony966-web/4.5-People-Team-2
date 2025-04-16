using UnityEngine;

[System.Serializable]
public struct InventoryItem
{
    [HideInInspector] public string itemName;
    public Sprite icon;
    [HideInInspector] public int quantity;

    public InventoryItem(string name, Sprite icon, int quantity = 1)
    {
        this.itemName = name;
        this.icon = icon;
        this.quantity = quantity;
    }
}