using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageManager : MonoBehaviour
{
    public static StorageManager Instance { get; set; }

    [SerializeField] public GameObject StorageUnitSmallUI;
    [SerializeField] StorageUnit selectedStorage;
    public bool storageUIOpen;
    public GameObject ItemSlotPrefab;

    public bool IsOpen;

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

    public void OpenBox(StorageUnit storage)
    {
        SetSelectedStorage(storage);
        PopulateStorage(GetRelevantUI(selectedStorage));
        GetRelevantUI(selectedStorage).SetActive(true);

        storageUIOpen = true;
        IsOpen = true;
    }

    private void PopulateStorage(GameObject storageUI)
    {
        // Get all slots of the ui
        List<GameObject> uiSlots = new List<GameObject>();

        foreach (Transform child in storageUI.transform)
        {
            uiSlots.Add(child.gameObject);
        }

        // Now, instantiate the prefab and set it as a child of each GameObject
        foreach (ItemAssets name in selectedStorage.Items)
        {
            foreach (GameObject slot in uiSlots)
            {
                if (slot.transform.childCount < 1)
                {
                    //Instantiate(name.ItemModel.GetComponent<InventoryItem>()
                    var itemToAdd = Instantiate(ItemSlotPrefab, slot.transform.position, slot.transform.rotation);
                    itemToAdd.transform.SetParent(slot.transform);
                    itemToAdd.GetComponent<InventoryItem>().ItemID = name;
                    break;
                }
            }
        }
    }

    public void CloseBox()
    {
        RecalculateStorage(GetRelevantUI(selectedStorage));
        GetRelevantUI(selectedStorage).SetActive(false);

        storageUIOpen = false;
        IsOpen = false;
    }

    private void RecalculateStorage(GameObject storageUI)
    {
        List<GameObject> uiSlots = new List<GameObject>();
        foreach(Transform child in storageUI.transform)
        {
            uiSlots.Add(child.gameObject);
        }

        selectedStorage.Items.Clear();

        List<GameObject> toBeDeleted = new List<GameObject>();

        foreach(GameObject slot in uiSlots)
        {
            if(slot.transform.childCount > 0)
            {
                selectedStorage.Items.Add(slot.transform.GetChild(0).GetComponent<InventoryItem>().ItemID);
                toBeDeleted.Add(slot.transform.GetChild(0).gameObject);
            }
        }

        foreach(GameObject obj in toBeDeleted)
        {
            Destroy(obj);
        }

    }

    public void SetSelectedStorage(StorageUnit storage)
    {
        selectedStorage = storage;
    }

    private GameObject GetRelevantUI(StorageUnit storage)
    {
        // Create a switch for other types
        return StorageUnitSmallUI;
    }
}