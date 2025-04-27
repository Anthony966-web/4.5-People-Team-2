using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ItemAssets", menuName = "Scriptable Objects/ItemAssets")]
[System.Serializable]
public class ItemAssets : ScriptableObject
{
    public Sprite ItemIcon;
    public string ItemName;
    public GameObject ItemModel;
    public int Quantity;

    [TextArea(3, 3)] public string ItemDescription;
    public bool IsDroppable;
    public bool IsEquippable;
    public bool IsConsumable;
    public bool IsUseable;
    public GameObject itemPendingToBeUsed; // Only if Useable

    public float healthEffect;
    public float hungerEffect;
}
