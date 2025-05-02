using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class InventoryItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    //public bool isDroppable;

    // ---- Item Info UI ---- //
    private GameObject itemInfoUI;

    private Image itemInfoUI_itemIcon;
    private TMP_Text itemInfoUI_itemName;
    private TMP_Text itemInfoUI_itemDescription;

    public ItemAssets ItemID;
    //public Image thisIcon;
    //public string thisName, thisDescription;

    // ---- Consumption ---- //
    public GameObject itemPendingConsumption;
    //public bool isConsumable;

    //public float healthEffect;
    //public float hungerEffect;

    // ---- Equipping ---- //
    //public bool isEquippable;
    private GameObject itemPendingEquipping;
    public bool isInsideQuickSlot;

    public bool isSelected;

    // ---- Construction ---- //
    //public GameObject itemPendingToBeUsed;

    void Start()
    {
        itemInfoUI = InventorySystem.Instance.ItemInfoUI;
        itemInfoUI_itemIcon = itemInfoUI.transform.Find("ItemIcon").GetComponent<Image>();
        itemInfoUI_itemName = itemInfoUI.transform.Find("ItemName").GetComponent<TMP_Text>();
        itemInfoUI_itemDescription = itemInfoUI.transform.Find("ItemDescription").GetComponent<TMP_Text>();
    }

    void Update()
    {
        if (ItemID != null && gameObject.name != ItemID.ItemName)
        {
            print(gameObject.name);
            gameObject.name = ItemID.ItemName;
            GetComponent<Image>().sprite = ItemID.ItemIcon;
        }

        if (isSelected)
        {
            gameObject.GetComponent<DragDrop>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<DragDrop>().enabled = true;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        itemInfoUI.SetActive(true);
        itemInfoUI_itemIcon.sprite = ItemID.ItemIcon;
        itemInfoUI_itemName.text = ItemID.ItemName;

        if(ItemID.IsConsumable)
        {
            itemInfoUI_itemDescription.text = ItemID.ItemDescription + ", +" + ItemID.healthEffect + " Health, +" + ItemID.hungerEffect + " Hunger.";
        }
        else
        {
            itemInfoUI_itemDescription.text = ItemID.ItemDescription;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        itemInfoUI.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            if(ItemID.IsConsumable)
            {
                itemPendingConsumption = gameObject;
                consumingFunction(ItemID.healthEffect, ItemID.hungerEffect);
            }

            if (ItemID.IsEquippable && isInsideQuickSlot == false && EquipSystem.Instance.CheckIfFull() == false)
            {
                EquipSystem.Instance.AddToQuickSlots(gameObject);
                isInsideQuickSlot = true;
            }

            if (ItemID.IsUseable)
            {
                //itemPendingToBeUsed = ItemID.itemPendingToBeUsed;
                ConstructionManager.Instance.itemToBeDestroyed = gameObject;
                gameObject.SetActive(false);
                UseItem();
            }
        }
    }

    private void UseItem()
    {
        itemInfoUI.SetActive(false);

        InventorySystem.Instance.isOpen = false;
        InventorySystem.Instance.inventoryScreenUI.SetActive(false);

        CraftingSystem.Instance.CraftingScreenUI.SetActive(false);
        CraftingSystem.Instance.ToolsScreenUI.SetActive(false);
        CraftingSystem.Instance.ConstructionScreenUI.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if(ItemID)
        {
            ConstructionManager.Instance.ActivateConstructionPlacement(ItemID);
        }

        //switch (ItemID.ItemName)
        //{
        //    case "Foundation":
        //        ConstructionManager.Instance.ActivateConstructionPlacement(ItemID);
        //        break;

        //    case "Wall":
        //        ConstructionManager.Instance.ActivateConstructionPlacement(ItemID);
        //        break;

        //    case "Roof":
        //        ConstructionManager.Instance.ActivateConstructionPlacement(ItemID);
        //        break;

        //    default:
        //        // Do Nothing
        //        print("No Item Found ):");
        //        break;
        //}
    }

    // Triggered when the mouse button is released over the item that has this script.
    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (ItemID.IsConsumable && itemPendingConsumption == gameObject)
            {
                DestroyImmediate(gameObject);
                InventorySystem.Instance.ReCalculateList();
                CraftingSystem.Instance.RefreshNeededItems();
            }
        }
    }

    public void consumingFunction(float healthEffect, float hungerEffect)
    {
        itemInfoUI.SetActive(false);

        healthEffectCalculation(healthEffect);

        hungerEffectCalculation(hungerEffect);

    }


    private static void healthEffectCalculation(float healthEffect)
    {
        // --- Health --- //

        float healthBeforeConsumption = PlayerState.Instance.currentHealth;
        float maxHealth = PlayerState.Instance.maxHealth;

        if (healthEffect != 0)
        {
            if ((healthBeforeConsumption + healthEffect) > maxHealth)
            {
                PlayerState.Instance.setHealth(maxHealth);
            }
            else
            {
                PlayerState.Instance.setHealth(healthBeforeConsumption + healthEffect);
            }
        }
    }


    private static void hungerEffectCalculation(float hungerEffect)
    {
        // --- Hunger --- //

        float hungerBeforeConsumption = PlayerState.Instance.currentHunger;
        float maxHunger = PlayerState.Instance.maxHunger;

        if (hungerEffect != 0)
        {
            if ((hungerBeforeConsumption + hungerEffect) > maxHunger)
            {
                PlayerState.Instance.setHunger(maxHunger);
            }
            else
            {
                PlayerState.Instance.setHunger(hungerBeforeConsumption + hungerEffect);
            }
        }
    }
}
