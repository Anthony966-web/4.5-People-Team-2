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
    Button CraftFoundationBTN;
    Button CraftWallBTN;
    Button CraftRoofBTN;
    Button CraftChestBTN;

    // Requirement Text
    TMP_Text AxeReq1, AxeReq2;
    TMP_Text FoundationReq1, FoundationReq2;
    TMP_Text WallReq1, WallReq2;
    TMP_Text RoofReq1, RoofReq2;
    TMP_Text ChestReq1, ChestReq2;

    bool isOpen;

    // All Blueprints
    public ItemBlueprint AxeBLP;
    public ItemBlueprint FoundationBLP;
    public ItemBlueprint WallBLP;
    public ItemBlueprint RoofBLP;
    public ItemBlueprint ChestBLP;


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

        //---- ToolsScreenUI ----//

        // Axe
        AxeReq1 = ToolsScreenUI.transform.Find("Contents").transform.Find("Axe").transform.Find("req1").GetComponent<TMP_Text>();
        AxeReq2 = ToolsScreenUI.transform.Find("Contents").transform.Find("Axe").transform.Find("req2").GetComponent<TMP_Text>();

        CraftAxeBTN = ToolsScreenUI.transform.Find("Contents").transform.Find("Axe").transform.Find("CraftButton").GetComponent<Button>();
        CraftAxeBTN.onClick.AddListener(delegate { CraftAnyItem(AxeBLP); });

        //---- ConstructionScreenUI ----//

        // Foundation
        FoundationReq1 = ConstructionScreenUI.transform.Find("Contents").transform.Find("Foundation").transform.Find("req1").GetComponent<TMP_Text>();
        FoundationReq2 = ConstructionScreenUI.transform.Find("Contents").transform.Find("Foundation").transform.Find("req2").GetComponent<TMP_Text>();

        CraftFoundationBTN = ConstructionScreenUI.transform.Find("Contents").transform.Find("Foundation").transform.Find("CraftButton").GetComponent<Button>();
        CraftFoundationBTN.onClick.AddListener(delegate { CraftAnyItem(FoundationBLP); });

        // Wall
        WallReq1 = ConstructionScreenUI.transform.Find("Contents").transform.Find("Wall").transform.Find("req1").GetComponent<TMP_Text>();
        WallReq2 = ConstructionScreenUI.transform.Find("Contents").transform.Find("Wall").transform.Find("req2").GetComponent<TMP_Text>();

        CraftWallBTN = ConstructionScreenUI.transform.Find("Contents").transform.Find("Wall").transform.Find("CraftButton").GetComponent<Button>();
        CraftWallBTN.onClick.AddListener(delegate { CraftAnyItem(WallBLP); });

        // Roof
        RoofReq1 = ConstructionScreenUI.transform.Find("Contents").transform.Find("Roof").transform.Find("req1").GetComponent<TMP_Text>();
        RoofReq2 = ConstructionScreenUI.transform.Find("Contents").transform.Find("Roof").transform.Find("req2").GetComponent<TMP_Text>();

        CraftRoofBTN = ConstructionScreenUI.transform.Find("Contents").transform.Find("Roof").transform.Find("CraftButton").GetComponent<Button>();
        CraftRoofBTN.onClick.AddListener(delegate { CraftAnyItem(RoofBLP); });

        // Chest
        ChestReq1 = ConstructionScreenUI.transform.Find("Contents").transform.Find("Chest").transform.Find("req1").GetComponent<TMP_Text>();
        ChestReq2 = ConstructionScreenUI.transform.Find("Contents").transform.Find("Chest").transform.Find("req2").GetComponent<TMP_Text>();

        CraftChestBTN = ConstructionScreenUI.transform.Find("Contents").transform.Find("Chest").transform.Find("CraftButton").GetComponent<Button>();
        CraftChestBTN.onClick.AddListener(delegate { CraftAnyItem(ChestBLP); });



        CraftingScreenUI.SetActive(false);
        ToolsScreenUI.SetActive(false);
        ConstructionScreenUI.SetActive(false);
        RefreshNeededItems();
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
            //Cursor.lockState = CursorLockMode.None;
        }
        else if (isOpen == false)
        {
            CraftingScreenUI.SetActive(false);
            ToolsScreenUI.SetActive(false);
            ConstructionScreenUI.SetActive(false);
            //Cursor.lockState = CursorLockMode.Locked;
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
        int stoneCount = itemCounts.ContainsKey("Stone") ? itemCounts["Stone"] : 0;
        int woodCount = itemCounts.ContainsKey("Wood") ? itemCounts["Wood"] : 0;

        // ---- AXE ---- //
        AxeReq1.text = $"{AxeBLP.ReqAmount[0]} {AxeBLP.Req[0].name} [{woodCount}]";
        AxeReq2.text = $"{AxeBLP.ReqAmount[1]} {AxeBLP.Req[1].name} [{stoneCount}]";

        // ---- Foundation ---- //
        FoundationReq1.text = $"{FoundationBLP.ReqAmount[0]} {FoundationBLP.Req[0].name} [{woodCount}]";
        FoundationReq2.text = "";

        // ---- Wall ---- //
        WallReq1.text = $"{WallBLP.ReqAmount[0]} {WallBLP.Req[0].name} [{woodCount}]";
        WallReq2.text = "";

        // ---- Roof ---- //
        RoofReq1.text = $"{RoofBLP.ReqAmount[0]} {RoofBLP.Req[0].name} [{woodCount}]";
        RoofReq2.text = "";

        // ---- Chest ---- //
        ChestReq1.text = $"{ChestBLP.ReqAmount[0]} {ChestBLP.Req[0].name} [{woodCount}]";
        ChestReq2.text = "";

        bool canCraftAxe = stoneCount >= AxeBLP.ReqAmount[0] && woodCount >= AxeBLP.ReqAmount[1] && InventorySystem.Instance.CheckSlotIsAvailable(1);
        bool canCraftFoundation = woodCount >= FoundationBLP.ReqAmount[0] && InventorySystem.Instance.CheckSlotIsAvailable(1);
        bool canCraftWall = woodCount >= WallBLP.ReqAmount[0] && InventorySystem.Instance.CheckSlotIsAvailable(1);
        bool canCraftRoof = woodCount >= RoofBLP.ReqAmount[0] && InventorySystem.Instance.CheckSlotIsAvailable(1);
        bool canCraftChest = woodCount >= ChestBLP.ReqAmount[0] && InventorySystem.Instance.CheckSlotIsAvailable(1);

        CraftAxeBTN.gameObject.SetActive(canCraftAxe);
        CraftFoundationBTN.gameObject.SetActive(canCraftFoundation);
        CraftWallBTN.gameObject.SetActive(canCraftWall);
        CraftRoofBTN.gameObject.SetActive(canCraftRoof);
        CraftChestBTN.gameObject.SetActive(canCraftChest);
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