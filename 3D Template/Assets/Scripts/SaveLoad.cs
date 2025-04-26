using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

[System.Serializable]
public class InventorySaveData
{
    public List<string> savedItemNames = new List<string>();
}

public class SaveLoad : MonoBehaviour
{
    public List<SavableObjects> savableObjects;
    public List<ItemAssets> ItemAssets;
    RandomPlacer randomPlacer;

    [SerializeField] public static string SlotKey = "None";
    public string FileType = ".txt";

    private void Get()
    {
        if (randomPlacer == null)
        {
            randomPlacer = FindObjectOfType<RandomPlacer>();
        }

        if (randomPlacer == null)
        {
            Debug.LogError("RandomPlacer not found in the scene!");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Save();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Load();
        }
    }

    public void Save()
    {
        ItemAssets = InventorySystem.Instance.itemList;

        InventorySaveData saveData = new InventorySaveData();

        foreach (var item in ItemAssets)
        {
            saveData.savedItemNames.Add(item.name); // save only the name
        }

        FileStream fs = File.Create(Application.persistentDataPath + "/Game.Data." + SlotKey + FileType);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(fs, saveData);
        fs.Close();
    }

    public bool Load()
    {
        string path = Application.persistentDataPath + "/Game.Data." + SlotKey + FileType;
        if (File.Exists(path))
        {
            long fileSize = new FileInfo(path).Length;
            Debug.Log($"File Size: {fileSize} bytes");

            FileStream fs = File.Open(path, FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            if (fs.Length > 0)
            {
                InventorySaveData saveData = (InventorySaveData)bf.Deserialize(fs);
                fs.Close();

                ItemAssets.Clear();

                foreach (var itemName in saveData.savedItemNames)
                {
                    ItemAssets foundItem = Resources.Load<ItemAssets>("Prefabs/Items/" + itemName);
                    if (foundItem != null)
                    {
                        ItemAssets.Add(foundItem);
                    }
                    else
                    {
                        Debug.LogWarning($"Item '{itemName}' not found in Resources/Prefabs/Items!");
                    }
                }

                InventorySystem.Instance.itemList = ItemAssets;
                return true;
            }
        }
        return false;
    }
}
