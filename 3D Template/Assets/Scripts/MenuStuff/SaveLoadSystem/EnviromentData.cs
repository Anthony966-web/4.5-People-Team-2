

using System.Collections.Generic;
using NUnit.Framework;

[System.Serializable]
public class EnviromentData
{
    public List<ItemAssets> pickedupItems;
    public EnviromentData(List<ItemAssets> _pickedupItems)
    {
        pickedupItems = _pickedupItems;
    }
}