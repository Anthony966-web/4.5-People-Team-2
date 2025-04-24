using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventoryItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public bool isDroppable;

    // ---- Item Info UI ---- //
    private GameObject itemInfoUI;

    private Image itemInfoUI_itemIcon;
    private TMP_Text itemInfoUI_itemName;
    private TMP_Text itemInfoUI_itemDescription;

    public Image thisIcon;
    public string thisName, thisDescription;

    // ---- Consumption ---- //
    private GameObject itemPendingConsumption;
    public bool isConsumable;

    public float healthEffect;
    public float hungerEffect;

    // ---- Equipping ---- //
    public bool isEquippable;
    private GameObject itemPendingEquipping;
    public bool isInsideQuickSlot;

    public bool isSelected;


    void Start()
    {
        itemInfoUI = InventorySystem.Instance.ItemInfoUI;
        itemInfoUI_itemIcon = itemInfoUI.transform.Find("ItemIcon").GetComponent<Image>();
        itemInfoUI_itemName = itemInfoUI.transform.Find("ItemName").GetComponent<TMP_Text>();
        itemInfoUI_itemDescription = itemInfoUI.transform.Find("ItemDescription").GetComponent<TMP_Text>();
    }

    void Update()
    {
        if(isSelected)
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
        itemInfoUI_itemIcon.sprite = thisIcon.sprite;
        itemInfoUI_itemName.text = thisName;
        itemInfoUI_itemDescription.text = thisDescription;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        itemInfoUI.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            if(isConsumable)
            {
                itemPendingConsumption = gameObject;
                consumingFunction(healthEffect, hungerEffect);
            }

            if (isEquippable && isInsideQuickSlot == false && EquipSystem.Instance.CheckIfFull() == false)
            {
                EquipSystem.Instance.AddToQuickSlots(gameObject);
                isInsideQuickSlot = true;
            }
        }
    }
    // Triggered when the mouse button is released over the item that has this script.
    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (isConsumable && itemPendingConsumption == gameObject)
            {
                DestroyImmediate(gameObject);
                InventorySystem.Instance.ReCalculateList();
                CraftingSystem.Instance.RefreshNeededItems();
            }
        }
    }

    private void consumingFunction(float healthEffect, float hungerEffect)
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
