using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipSystem : MonoBehaviour
{
    public static EquipSystem Instance { get; set; }

    // -- UI -- //
    public GameObject quickSlotsPanel;

    public List<GameObject> quickSlotsList = new List<GameObject>();
    //public List<string> itemList = new List<string>();

    public GameObject numbersHolder;

    public int selectedNumber = -1;
    public GameObject selectedItem;

    public GameObject ToolHolder;

    public GameObject selectedItemModel;


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


    private void Start()
    {
        PopulateSlotList();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectQuickSlot(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectQuickSlot(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectQuickSlot(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SelectQuickSlot(4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SelectQuickSlot(5);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SelectQuickSlot(6);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            SelectQuickSlot(7);
        }
    }

    void SelectQuickSlot(int number)
    {
        if(checkIfSlotIsFull(number) == true)
        {
            if(selectedNumber != number)
            {
                selectedNumber = number;

                if (selectedItem != null)
                {
                    selectedItem.gameObject.GetComponent<InventoryItem>().isSelected = false;
                }

                selectedItem = GetSelectedItem(number);
                selectedItem.GetComponent<InventoryItem>().isSelected = true;

                // Another Check For Consumable
                if (selectedItem.GetComponent<InventoryItem>().ItemID.IsConsumable == true)
                {
                    selectedItem.GetComponent<InventoryItem>().itemPendingConsumption = gameObject;
                    selectedItem.GetComponent<InventoryItem>().consumingFunction(selectedItem.GetComponent<InventoryItem>().ItemID.healthEffect, selectedItem.GetComponent<InventoryItem>().ItemID.hungerEffect);

                    if (selectedItem.GetComponent<InventoryItem>().itemPendingConsumption == gameObject)
                    {
                        DestroyImmediate(selectedItem.gameObject);
                        InventorySystem.Instance.ReCalculateList();
                        CraftingSystem.Instance.RefreshNeededItems();
                    }
                    selectedItem = null;
                    selectedNumber = -1;
                    return;
                }
                
                SetEquippedModel(selectedItem);


                foreach (Transform child in numbersHolder.transform)
                {
                    child.GetComponent<TMP_Text>().color = Color.white;
                }

                TMP_Text ToBeChanged = numbersHolder.transform.Find("number" + number).GetComponent<TMP_Text>();
                ToBeChanged.color = Color.yellow;
            }
            else
            {
                selectedNumber = -1;

                if (selectedItem != null)
                {
                    selectedItem.gameObject.GetComponent<InventoryItem>().isSelected = false;
                    selectedItem = null;
                }

                if(selectedItemModel != null)
                {
                    DestroyImmediate(selectedItemModel.gameObject);
                    selectedItemModel = null;
                }

                foreach (Transform child in numbersHolder.transform)
                {
                    child.GetComponent<TMP_Text>().color = Color.white;
                }
            }
        }
    }

    private void SetEquippedModel(GameObject SelectedItemModel)
    {
        if (selectedItemModel != null)
        {
            DestroyImmediate(selectedItemModel.gameObject);
            selectedItemModel = null;
        }

        selectedItemModel = Instantiate(SelectedItemModel.GetComponent<InventoryItem>().ItemID.ItemModel,
            new Vector3(0.6f, 0f, 0.04f), Quaternion.Euler(0, -12.5f, -20f));
        selectedItemModel.name = SelectedItemModel.name;
        selectedItemModel.transform.SetParent(ToolHolder.transform, false);
    }

    private GameObject GetSelectedItem(int slotNumber)
    {
        return quickSlotsList[slotNumber - 1].transform.GetChild(0).gameObject;
    }

    private bool checkIfSlotIsFull(int slotNumber)
    {
        if (quickSlotsList[slotNumber - 1].transform.childCount > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void PopulateSlotList()
    {
        foreach (Transform child in quickSlotsPanel.transform)
        {
            if (child.CompareTag("QuickSlot"))
            {
                quickSlotsList.Add(child.gameObject);
            }
        }
    }

    public void AddToQuickSlots(GameObject itemToEquip)
    {
        // Find next free slot
        GameObject availableSlot = FindNextEmptySlot();
        // Set transform of our object
        itemToEquip.transform.SetParent(availableSlot.transform, false);
        // Getting clean name
        string cleanName = itemToEquip.name.Replace("(Clone)", "");
        // Adding item to list
        //itemList.Add(cleanName);

        InventorySystem.Instance.ReCalculateList();

    }


    private GameObject FindNextEmptySlot()
    {
        foreach (GameObject slot in quickSlotsList)
        {
            if (slot.transform.childCount == 0)
            {
                return slot;
            }
        }
        return new GameObject();
    }

    public bool CheckIfFull()
    {

        int counter = 0;

        foreach (GameObject slot in quickSlotsList)
        {
            if (slot.transform.childCount > 0)
            {
                counter += 1;
            }
        }

        if (counter == quickSlotsList.Count)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}