using UnityEngine;

[CreateAssetMenu(fileName = "ItemAssets", menuName = "Scriptable Objects/ItemAssets")]
public class ItemAssets : ScriptableObject
{
    public InventoryItem inventoryItem;

    public void OnValidate()
    {
        inventoryItem.itemName = name;
        inventoryItem.quantity = 1;
    }
}
