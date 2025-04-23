using UnityEngine;

public class ItemBlueprint : MonoBehaviour
{
    public string itemname;

    public string Req1;
    public string Req2;

    public int Req1Amount;
    public int Req2Amount;

    public int numOfRequirements;

    public ItemBlueprint(string name, int reqNUM, string R1, int R1num, string R2, int R2num)
    {
        itemname = name;

        numOfRequirements = reqNUM;

        Req1 = R1;
        Req2 = R2;

        Req1Amount = R1num;
        Req2Amount = R2num;
    }
}
