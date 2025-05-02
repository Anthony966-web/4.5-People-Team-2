using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryDropZone : MonoBehaviour, IDropHandler
{
    public static event Action<GameObject> OnItemDroppedInZone;

    public Transform playerTransform; // Assign this in the Inspector
    public float dropDistance = 2f;   // How far in front of the player to drop the item

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedItem = eventData.pointerDrag;

        if (droppedItem != null)
        {
            Debug.Log("Item dropped on panel: " + droppedItem.name);
            OnItemDroppedInZone?.Invoke(droppedItem);
        }
    }

    private void OnEnable()
    {
        InventoryDropZone.OnItemDroppedInZone += HandleItemDrop;
    }

    private void OnDisable()
    {
        InventoryDropZone.OnItemDroppedInZone -= HandleItemDrop;
    }

    private void HandleItemDrop(GameObject droppedItem)
    {
        Debug.Log("Handling dropped item: " + droppedItem.name);

        if(droppedItem.GetComponent<InventoryItem>().ItemID.IsDroppable)
        {
            if(droppedItem.GetComponent<InventoryItem>().ItemID.ItemObject == null)
            {
                print(droppedItem.name + " + " + "Has no ItemModel To Drop");
            }
            // Calculate drop position in front of player
            Vector3 dropPosition = playerTransform.position + playerTransform.forward * dropDistance;

            // Instantiate the world version of the item
            GameObject worldItem = Instantiate(droppedItem.GetComponent<InventoryItem>().ItemID.ItemObject.gameObject, dropPosition, Quaternion.identity);
            worldItem.name = worldItem.GetComponent<INventoryObject>().inventoryItem.ItemName;
            // Destroy the inventory UI version
            Destroy(droppedItem);
            InventorySystem.Instance.IsDraggingItem = false;
            droppedItem = null;

            // Optionally refresh inventory or crafting UI
            InventorySystem.Instance.ReCalculateList();
            CraftingSystem.Instance.RefreshNeededItems();
        }
    }
}