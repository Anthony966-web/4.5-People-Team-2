using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSystem : MonoBehaviour
{
    public ItemAssets[] availableItems;
    public GameObject shopUIPrefab;
    public Transform shopUIParent;

    void Start()
    {  
        foreach (var item in availableItems)
        {
            GameObject uiItem = Instantiate(shopUIPrefab, shopUIParent);

            item.Value = item.Cost * 0.8f;

            uiItem.transform.Find("Name").GetComponent<TMP_Text>().text = item.ItemName;
            uiItem.GetComponent<Image>().sprite = item.ItemIcon;
            uiItem.transform.Find("Cost").GetComponent<TMP_Text>().text = "Cost: $" + item.Cost.ToString();
            uiItem.transform.Find("Value").GetComponent<TMP_Text>().text = "Value: $" + item.Value.ToString();

            Button buyButton = uiItem.transform.Find("BuyButton").GetComponent<Button>();
            buyButton.onClick.AddListener(() => BuyItem(item));

            Button sellButton = uiItem.transform.Find("SellButton").GetComponent<Button>();
            sellButton.onClick.AddListener(() => SellItem(item));
        }
    }


    void BuyItem(ItemAssets item)
    {
        if (PlayerState.Instance.SpendMoney(item.Cost))
        {
            InventorySystem.Instance.AddToInventory(item, item.Quantity);
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
            DestroyImmediate(item);
            InventorySystem.Instance.ReCalculateList();
            CraftingSystem.Instance.RefreshNeededItems();
        }
    }
}