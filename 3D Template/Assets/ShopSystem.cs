using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSystem : MonoBehaviour
{
    public ItemAssets[] availableItems;
    public GameObject shopUIPrefab;
    public Transform shopUIParent;

    public Button buyButton;
    public Button sellButton;

    void Start()
    {
        foreach (var item in availableItems)
        {
            GameObject uiItem = Instantiate(shopUIPrefab, shopUIParent);

            float value = item.Cost * 0.8f;

            uiItem.transform.Find("Name").GetComponent<TMP_Text>().text = item.ItemName;
            uiItem.GetComponent<Image>().sprite = item.ItemIcon;
            uiItem.transform.Find("Cost").GetComponent<TMP_Text>().text = $"Cost: ${item.Cost}";
            uiItem.transform.Find("Value").GetComponent<TMP_Text>().text = $"Value: ${value}";

            var buyButton = uiItem.transform.GetChild(3).GetComponent<Button>();
            buyButton.onClick.AddListener(() => BuyItem(item));

            var sellButton = uiItem.transform.GetChild(4).GetComponent<Button>();
            sellButton.onClick.AddListener(() => SellItem(item));

            if (buyButton == null) Debug.LogError("Buy button not found!");
            if (sellButton == null) Debug.LogError("Sell button not found!");

        }

    }


    void BuyItem(ItemAssets item)
    {
        print("Works");
        if (PlayerState.Instance.SpendMoney(item.Cost))
        {
            InventorySystem.Instance.AddToInventory(item, 1);
            InventorySystem.Instance.ReCalculateList();
            CraftingSystem.Instance.RefreshNeededItems();
        }
        else
        {
            Debug.Log("Not Enough Money's And W Riz!");
        }
    }

    void SellItem(ItemAssets item)
    {
        if(item != null)
        {
            PlayerState.Instance.AddMoney(item.Value);
            InventorySystem.Instance.RemoveItem(item, 1);
            InventorySystem.Instance.ReCalculateList();
            CraftingSystem.Instance.RefreshNeededItems();
        }
    }
}