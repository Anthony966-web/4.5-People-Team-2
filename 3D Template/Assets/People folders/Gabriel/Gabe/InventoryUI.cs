using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    //public Inventory inventory;
    //public Transform itemParent;
    //public GameObject itemSlotPrefab;

    //public GameObject iventoryFrame;
    //private bool InventoryOpen;

    //public Transform[] Items;

    //private void Start()
    //{
    //    inventory.inventoryChangedCallback += UpdateUI;
    //    UpdateUI();
    //}
    //void UpdateUI()
    //{
    //    foreach (Transform child in itemParent)
    //    {
    //        if (child.childCount > 0)
    //            Destroy(child.GetChild(0).gameObject);
    //    }

    //    for (int i = 0; i < inventory.items.Count; i++)
    //    {
    //        GameObject slot = Instantiate(itemSlotPrefab, Items[i]);

    //        if (inventory.items[i].quantity <= 1)
    //        {
    //            slot.transform.Find("Quantity").GetComponent<TMP_Text>().text = "";
    //        }
    //        else
    //        {
    //            slot.transform.Find("Quantity").GetComponent<TMP_Text>().text = "x" + inventory.items[i].quantity;
    //        }

    //        slot.transform.Find("Name").GetComponent<TMP_Text>().text = inventory.items[i].itemName;
    //        slot.transform.Find("Icon").GetComponent<Image>().sprite = inventory.items[i].icon;

    //        print(inventory.items[i].itemName);
    //        print(inventory.items[i].quantity);
    //    }
    //}

    //public void Update()
    //{
    //  if(Input.GetKeyUp(KeyCode.I))
    //    {
    //        if (InventoryOpen)
    //        {
    //            iventoryFrame.SetActive(false);
    //            InventoryOpen = false;
    //            print(InventoryOpen);
    //            return;
    //        }
    //        else
    //        {
    //            iventoryFrame.SetActive(true);
    //            InventoryOpen = true;
    //            print(InventoryOpen);
    //            return;
    //        }
           
    //    }
    //}
}