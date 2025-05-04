using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ItemAssets", menuName = "Scriptable Objects/ItemAssets")]
[System.Serializable]
public class ItemAssets : ScriptableObject
{
    public Sprite ItemIcon;
    public string ItemName;
    public GameObject ItemModel; // Equipable
    public GameObject ItemObject; // Ground GameObject
    [HideInInspector]   public int Quantity = 1;

    [TextArea(3, 3)] public string ItemDescription;
    public bool IsDroppable;
    public bool IsEquippable;
    public bool IsPickaxe;
    public bool IsSword;
    public bool IsAxe;
    public float Damage;

    //  Value and cost
    public float Cost;
    [HideInInspector]   public float Value;

    public bool IsConsumable;
    public bool IsUseable;
    public GameObject itemPendingToBeUsed; // Only if Useable

    public float healthEffect;
    public float hungerEffect;
}
