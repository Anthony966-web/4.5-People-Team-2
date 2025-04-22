using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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



    public static CraftingSystem instance { get; set; }

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }


    void Start()
    {
        isOpen = false;
        ToolsBTN = CraftingScreenUI.transform.Find("Contents").Find("ToolsButton").GetComponent<Button>();
        ToolsBTN.onClick.AddListener(delegate { OpenToolsCategory(); });




        CraftingScreenUI.SetActive(false);
        ToolsScreenUI.SetActive(false);
    }

    
    private void OpenToolsCategory()
    {
        CraftingScreenUI.SetActive(false);
        ToolsScreenUI.SetActive(true);
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
}
