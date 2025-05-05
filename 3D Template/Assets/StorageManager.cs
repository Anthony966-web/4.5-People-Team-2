using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageManager : MonoBehaviour
{
    public static StorageManager Instance { get; set; }

    [SerializeField] GameObject StorageUnitSmallUI;
    [SerializeField] StorageUnit selectedStorage;
    public bool storageUIOpen;
    public GameObject ItemSlotPrefab;

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

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        //SelectionManager.Instance.DisableSelection();
        //SelectionManager.Instance.GetComponent<SelectionManager>().enabled = false;
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
        GetRelevantUI(selectedStorage).SetActive(false);
        storageUIOpen = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //SelectionManager.Instance.EnableSelection();
        //SelectionManager.Instance.GetComponent<SelectionManager>().enabled = true;
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