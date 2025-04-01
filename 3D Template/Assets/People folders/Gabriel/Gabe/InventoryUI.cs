using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory;
    public Transform itemParent;
    public GameObject itemSlotPrefab;

    private void Start()
    {
        inventory.inventoryChangedCallback += UpdateUI;
        UpdateUI();
    }

    void UpdateUI()
    {
        foreach (Transform child in itemParent)
        {
            Destroy(child.gameObject);
        }

        foreach (var item in inventory.items)
        {
            GameObject slot = Instantiate(itemSlotPrefab, itemParent);
            slot.transform.Find("ItemName").GetComponent<Text>().text = item.itemName;
            slot.transform.Find("ItemQuantity").GetComponent<Text>().text = "x" + item.quantity;
            slot.transform.Find("ItemIcon").GetComponent<Image>().sprite = item.icon;
        }
    }
}