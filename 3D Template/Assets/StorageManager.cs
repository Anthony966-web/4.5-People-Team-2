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

    public float OriMoveSpeed;

    //public bool IsOpen;

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
        OriMoveSpeed = PlayerState.Instance.playerBody.GetComponent<CharacterMovement>().moveSpeed;
    }

    public void OpenBox(StorageUnit storage)
    {
        SetSelectedStorage(storage);
        print(selectedStorage);
        PopulateStorage(GetRelevantUI(selectedStorage).transform.GetChild(0).gameObject);
        GetRelevantUI(selectedStorage).SetActive(true);

        storageUIOpen = true;
        InventorySystem.Instance.isOpen = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        print(selectedStorage);

        PlayerState.Instance.playerBody.GetComponent<CharacterMovement>().moveSpeed = 0;
        PlayerState.Instance.playerBody.GetComponent<CharacterMovement>().enabled = false;
    }

    private void PopulateStorage(GameObject storageUI)
    {
        print(selectedStorage);
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
                print(slot.transform.childCount);
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
        print(selectedStorage);
        RecalculateStorage(GetRelevantUI(selectedStorage).transform.GetChild(0).gameObject);
        GetRelevantUI(selectedStorage).SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        storageUIOpen = false;
        InventorySystem.Instance.isOpen = false;

        PlayerState.Instance.playerBody.GetComponent<CharacterMovement>().enabled = true;
        PlayerState.Instance.playerBody.GetComponent<CharacterMovement>().moveSpeed = OriMoveSpeed;
    }

    private void RecalculateStorage(GameObject storageUI)
    {
        print(selectedStorage);
        print(storageUI);
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
                print(slot.transform.GetChild(0).GetComponent<InventoryItem>().ItemID.name);
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