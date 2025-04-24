using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemBlueprint", menuName = "Scriptable Objects/ItemBlueprint")]
public class ItemBlueprint : ScriptableObject
{
    public ItemAssets itemname;

    public List<ItemAssets> Req;
    public List<int> ReqAmount;
}
