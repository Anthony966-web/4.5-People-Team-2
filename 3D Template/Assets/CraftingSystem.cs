using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CraftingSystem : MonoBehaviour
{

    public GameObject CraftingScreenUI;
    public GameObject ToolsScreenUI;

    public List<string> InventoryItemList = new List<string>();

    // Category Buttons
    Button ToolsBTN;

    // Craft Buttons
    Button CraftAxeBTN;

    // Requirement Text
    TMP_Text AxeReq1, AxeReq2;

    bool isOpen;

    // All Blueprints
    public ItemBlueprint AxeBLP = new ItemBlueprint("Axe", 2, "Stone", 3, "Stick", 3);


    public static CraftingSystem Instance { get; set; }

    private void Awake()
    {
        if(Instance != null && Instance != this)
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
        isOpen = false;
        ToolsBTN = CraftingScreenUI.transform.Find("Contents").Find("ToolsButton").GetComponent<Button>();
        ToolsBTN.onClick.AddListener(delegate { OpenToolsCategory(); });

        // Axe
        AxeReq1 = ToolsScreenUI.transform.Find("Contents").transform.Find("Axe").transform.Find("req1").GetComponent<TMP_Text>();
        AxeReq2 = ToolsScreenUI.transform.Find("Contents").transform.Find("Axe").transform.Find("req2").GetComponent<TMP_Text>();

        CraftAxeBTN = ToolsScreenUI.transform.Find("Contents").transform.Find("Axe").transform.Find("CraftButton").GetComponent<Button>();
        CraftAxeBTN.onClick.AddListener(delegate { CraftAnyItem(AxeBLP); });


        CraftingScreenUI.SetActive(false);
        ToolsScreenUI.SetActive(false);
    }


    private void OpenToolsCategory()
    {
        CraftingScreenUI.SetActive(false);
        ToolsScreenUI.SetActive(true);
    }

    private void CraftAnyItem(ItemBlueprint craftBlueprint)
    {
        // Add Crafted Item Into Inventory
        InventorySystem.Instance.AddToInventory(craftBlueprint.itemname);

        // Remove Resources From Inventory
        if(craftBlueprint.numOfRequirements == 1)
        {
            InventorySystem.Instance.RemoveItem(craftBlueprint.Req1, craftBlueprint.Req1Amount);
        }
        else if (craftBlueprint.numOfRequirements == 2)
        {
            InventorySystem.Instance.RemoveItem(craftBlueprint.Req1, craftBlueprint.Req1Amount);
            InventorySystem.Instance.RemoveItem(craftBlueprint.Req2, craftBlueprint.Req2Amount);
        }

        //if (craftBlueprint.numOfRequirements == 3)
        //{
        //    InventorySystem.Instance.RemoveItem(craftBlueprint.Req3, craftBlueprint.Req3Amount);
        //    InventorySystem.Instance.RemoveItem(craftBlueprint.Req3, craftBlueprint.Req3Amount);
        //}

        StartCoroutine(calculate());

        // Refresh List
        //InventorySystem.Instance.ReCalculateList();
        
    }

    public IEnumerator calculate()
    {
        yield return 0;
        InventorySystem.Instance.ReCalculateList();
        RefreshNeededItems();
    }


    void Update()
    {

        if (Input.GetKeyDown(KeyCode.I) && !isOpen)
        {
            CraftingScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            isOpen = true;

        }
        else if (Input.GetKeyDown(KeyCode.I) && isOpen)
        {
            CraftingScreenUI.SetActive(false);
            ToolsScreenUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            isOpen = false;
        }
    }


    public void RefreshNeededItems()
    {
        int stone_count = 0;
        int stick_count = 0;

        InventoryItemList = InventorySystem.Instance.itemList;

        foreach(string itemname in InventoryItemList)
        {

            switch(itemname)
            {
                case "Stone":
                    stone_count += 1;
                break;
                case "Stick":
                    stick_count += 1;
                break;

            }
        }


        //---- AXE ----//
        AxeReq1.text =  AxeBLP.Req1Amount + " " + AxeBLP.Req1 + " [" + stone_count + "]";
        AxeReq2.text = AxeBLP.Req2Amount + " " + AxeBLP.Req2 + " [" + stick_count + "]";

        if (stone_count >= AxeBLP.Req1Amount &&  stick_count >= AxeBLP.Req2Amount)
        {
            CraftAxeBTN.gameObject.SetActive(true);
        }
        else
        {
            CraftAxeBTN.gameObject.SetActive(false);
        }
    }
}



//using UnityEngine;

//[CreateAssetMenu(fileName = "New Blueprint", menuName = "Inventory/Blueprint")]
//public class Blueprint : ScriptableObject
//{
//    public string itemName;
//    public string req1;
//    public string req2;
//    public int req1Amount;
//    public int req2Amount;
//    public int numOfRequirements;

//}