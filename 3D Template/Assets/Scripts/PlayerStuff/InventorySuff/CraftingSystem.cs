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
    public GameObject ConstructionScreenUI;

    public List<ItemAssets> InventoryItemList = new List<ItemAssets>();

    // Category Buttons
    Button ToolsBTN;
    Button CunstructionBTN;

    // Craft Buttons
    Button CraftAxeBTN;

    // Requirement Text
    TMP_Text AxeReq1, AxeReq2;

    bool isOpen;

    // All Blueprints
    public ItemBlueprint AxeBLP;
    public ItemBlueprint AxeBLP;


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

        CunstructionBTN = CraftingScreenUI.transform.Find("Contents").Find("ConstructionButton").GetComponent<Button>();
        CunstructionBTN.onClick.AddListener(delegate { OpenConstructionCategory(); });

        // Axe
        AxeReq1 = ToolsScreenUI.transform.Find("Contents").transform.Find("Axe").transform.Find("req1").GetComponent<TMP_Text>();
        AxeReq2 = ToolsScreenUI.transform.Find("Contents").transform.Find("Axe").transform.Find("req2").GetComponent<TMP_Text>();

        CraftAxeBTN = ToolsScreenUI.transform.Find("Contents").transform.Find("Axe").transform.Find("CraftButton").GetComponent<Button>();
        CraftAxeBTN.onClick.AddListener(delegate { CraftAnyItem(AxeBLP); });


        CraftingScreenUI.SetActive(false);
        ToolsScreenUI.SetActive(false);
        ConstructionScreenUI.SetActive(false);
    }


    private void OpenToolsCategory()
    {
        CraftingScreenUI.SetActive(false);
        ToolsScreenUI.SetActive(true);
    }

    private void OpenConstructionCategory()
    {
        CraftingScreenUI.SetActive(false);
        ConstructionScreenUI.SetActive(true);
    }

    private void CraftAnyItem(ItemBlueprint craftBlueprint)
    {
        // Add Crafted Item Into Inventory
        InventorySystem.Instance.AddToInventory(craftBlueprint.itemname, craftBlueprint.ProduceAmount);

        // Remove Resources From Inventory
        for (int i = 0; i < craftBlueprint.Req.Count; i++)
        {
            InventorySystem.Instance.RemoveItem(craftBlueprint.Req[i], craftBlueprint.ReqAmount[i]);
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
        isOpen = InventorySystem.Instance.isOpen;

        if (isOpen == true && !ConstructionManager.Instance.inConstructionMode)
        {
            CraftingScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;

        }
        else if (isOpen == false)
        {
            CraftingScreenUI.SetActive(false);
            ToolsScreenUI.SetActive(false);
            ConstructionScreenUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
    }


    //public void RefreshNeededItems()
    //{
    //    int stone_count = 0;
    //    int stick_count = 0;

    //    InventoryItemList = InventorySystem.Instance.itemList;

    //    foreach(ItemAssets itemname in InventoryItemList)
    //    {

    //        switch(itemname.ItemName)
    //        {
    //            case "Orange":
    //                stone_count += 1;
    //            break;
    //            case "Apple":
    //                stick_count += 1;
    //            break;

    //        }
    //    }


    //    //---- AXE ----//
    //    AxeReq1.text = AxeBLP.ReqAmount[0] + " " + AxeBLP.Req[0].name + " [" + stone_count + "]";
    //    AxeReq2.text = AxeBLP.ReqAmount[1] + " " + AxeBLP.Req[1].name + " [" + stick_count + "]";

    //    if (stone_count >= AxeBLP.ReqAmount[0] &&  stick_count >= AxeBLP.ReqAmount[1] && InventorySystem.Instance.CheckSlotIsAvailable(1))
    //    {
    //        CraftAxeBTN.gameObject.SetActive(true);
    //    }
    //    else
    //    {
    //        CraftAxeBTN.gameObject.SetActive(false);
    //    }
    //}


    public void RefreshNeededItems()
    {
        Dictionary<string, int> itemCounts = new Dictionary<string, int>();

        var inventory = InventorySystem.Instance.itemList;

        // Count all items
        foreach (ItemAssets item in inventory)
        {
            if (itemCounts.ContainsKey(item.ItemName))
                itemCounts[item.ItemName]++;
            else
                itemCounts[item.ItemName] = 1;
        }

        // Get counts safely
        int stoneCount = itemCounts.ContainsKey("Orange") ? itemCounts["Orange"] : 0;
        int stickCount = itemCounts.ContainsKey("Apple") ? itemCounts["Apple"] : 0;

        // ---- AXE ---- //
        AxeReq1.text = $"{AxeBLP.ReqAmount[0]} {AxeBLP.Req[0].name} [{stoneCount}]";
        AxeReq2.text = $"{AxeBLP.ReqAmount[1]} {AxeBLP.Req[1].name} [{stickCount}]";

        bool canCraftAxe = stoneCount >= AxeBLP.ReqAmount[0] && stickCount >= AxeBLP.ReqAmount[1] && InventorySystem.Instance.CheckSlotIsAvailable(1);

        CraftAxeBTN.gameObject.SetActive(canCraftAxe);
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