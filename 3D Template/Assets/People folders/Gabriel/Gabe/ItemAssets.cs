using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ItemAssets", menuName = "Scriptable Objects/ItemAssets")]
public class ItemAssets : ScriptableObject
{
    public Sprite ItemIcon;
    public string ItemName;
    public int Quantity;

    [TextArea(3, 3)] public string Description;
    public bool IsDroppable;
    public bool IsConsumable;

    public float healthEffect;
    public float hungerEffect;
}
