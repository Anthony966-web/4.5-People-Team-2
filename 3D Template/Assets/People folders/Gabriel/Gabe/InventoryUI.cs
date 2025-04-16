using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory;
    public Transform itemParent;
    public GameObject itemSlotPrefab;

    //public GameObject iventoryFrame;
    private bool InventoryOpen;

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
            slot.transform.Find("Name").GetComponent<TMP_Text>().text = item.itemName;
            slot.transform.Find("Quantity").GetComponent<TMP_Text>().text = "x" + item.quantity;
            slot.transform.Find("Icon").GetComponent<Image>().sprite = item.icon;

            print(item.itemName);
            print(item.quantity);
        }
    }

    public void Update()
    {
      if(Input.GetKeyUp(KeyCode.I))
        {
            if (InventoryOpen)
            {
                itemParent.gameObject.SetActive(false);
                InventoryOpen = false;
                print(InventoryOpen);
                return;
            }
            else
            {
                itemParent.gameObject.SetActive(true);
                InventoryOpen = true;
                print(InventoryOpen);
                return;
            }
           
        }
    }
}