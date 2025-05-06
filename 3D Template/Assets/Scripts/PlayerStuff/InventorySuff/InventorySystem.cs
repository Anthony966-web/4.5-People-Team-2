using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{

    public static InventorySystem Instance { get; set; }

    public GameObject inventoryScreenUI;
    public GameObject ItemInfoUI;

    public List<GameObject> slotList = new List<GameObject>();

    public List<ItemAssets> itemList = new List<ItemAssets>();

    private GameObject itemToAdd;

    private GameObject whatSlotToEquip;

    public GameObject ItemSlotPrefab;

    public bool isOpen;

    public bool IsDraggingItem;

    //public bool isFull;


    // Pickup Popup
    public GameObject pickupAlert;
    public TMP_Text pickupName;
    public Image pickupIcon;



    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }


    void Start()
    {
        inventoryScreenUI.SetActive(false);
        isOpen = false;
        IsDraggingItem = false;
        //isFull = false;

        PopulateSlotList();

        Cursor.visible = false;

    }

    private void PopulateSlotList()
    {
        foreach (Transform child in inventoryScreenUI.transform.Find("Contents").transform)
        {
            if (child.CompareTag("Slot"))
            {
                slotList.Add(child.gameObject);
            }
        }
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !ConstructionManager.Instance.inConstructionMode && !StorageManager.Instance.IsOpen)
        {
            isOpen = !isOpen;
        }

        if (isOpen)
        {
            inventoryScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            inventoryScreenUI.SetActive(false);

            // Only hide cursor if storage is also closed
            if (!StorageManager.Instance.IsOpen)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

    }

    public void AddToInventory(ItemAssets itemName, int Amount)
    {
        Debug.Log("Added " + itemName);

        for (int i = 0; i < Amount; i++)
        {

            whatSlotToEquip = FindNextEmptySlot();

            itemToAdd = Instantiate(ItemSlotPrefab, whatSlotToEquip.transform.position, whatSlotToEquip.transform.rotation);
            itemToAdd.GetComponent<InventoryItem>().ItemID = itemName;

            itemToAdd.transform.SetParent(whatSlotToEquip.transform);

            itemList.Add(itemName);

            TriggerPickupPopup(itemName.ItemName, itemName.ItemIcon);

            ReCalculateList();
            CraftingSystem.Instance.RefreshNeededItems();
        }
    }


    private GameObject FindNextEmptySlot()
    {
        foreach(GameObject slot in slotList)
        {
            if (slot.transform.childCount == 0)
            {
                return slot;
            }
        }

        return new GameObject();
    }

    public bool CheckSlotIsAvailable(int emptyNeeded)
    {
        int emptySlot = 0;

        foreach(GameObject slot in slotList)
        {
            if(slot.transform.childCount <= 0)
            {
                emptySlot += 1;
            }
        }

        if (emptySlot >= emptyNeeded)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    public void RemoveItem(ItemAssets nameToRemove, int amountToRemove)
    {
        int counter = amountToRemove;

        for(var i = slotList.Count - 1; i >= 0; i--)
        {
            if (slotList[i].transform.childCount > 0)
            {
                InventoryItem invItem = slotList[i].transform.GetChild(0).GetComponent<InventoryItem>();

                if (invItem != null && invItem.ItemID == nameToRemove && counter > 0)
                {
                    DestroyImmediate(slotList[i].transform.GetChild(0).gameObject);
                    counter--;
                }
            }
        }

        ReCalculateList();
        CraftingSystem.Instance.RefreshNeededItems();
    }

    void TriggerPickupPopup(string itemName, Sprite itemIcon)
    {
        pickupName.text = itemName;
        pickupIcon.sprite = itemIcon;
        pickupAlert.SetActive(true);

        StartCoroutine(Gone(2.25f));
    }

IEnumerator Gone(float Time)
    {
        yield return new WaitForSeconds(Time);
        pickupAlert.SetActive(false);
    }    


    public void ReCalculateList()
    {
        itemList.Clear();

        foreach(GameObject slot in slotList)
        {
            if(slot.transform.childCount > 0)
            {
                ItemAssets name = slot.transform.GetChild(0).GetComponent<InventoryItem>().ItemID;

                //string str1 = "(Clone)";
                //ItemAssets result = name.name.Replace(str1, "");
                ItemAssets Item = name;
                //Item = name.ItemName;

                itemList.Add(Item);
            }
        }
    }
}