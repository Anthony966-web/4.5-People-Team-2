using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

public class SaveLoad : MonoBehaviour
{

    public List<SavableObjects> savableObjects;
    RandomPlacer randomPlacer;

    public static string SlotKey = "None";
    public string FileType = ".txt";

    private void Get()
    {
        // Ensure RandomPlacer is assigned properly
        if (randomPlacer == null)
        {
            randomPlacer = FindObjectOfType<RandomPlacer>();
        }

        if (randomPlacer == null)
        {
            Debug.LogError("RandomPlacer not found in the scene!");
        }
    }

    //public void Save()
    //{
    //    Get();
    //    if (randomPlacer == null)
    //    {
    //        Debug.LogError("Save failed: RandomPlacer reference is null.");
    //        return;
    //    }

    //    savableObjects = randomPlacer.savableObjects;

    //    FileStream fs = File.Create(Application.persistentDataPath + "/Game.Data.bat");
    //    BinaryFormatter bf = new BinaryFormatter();
    //    bf.Serialize(fs, savableObjects);
    //    fs.Close();
    //}

    //public bool Load()
    //{

    //    if (randomPlacer == null)
    //    {
    //        randomPlacer = FindObjectOfType<RandomPlacer>(); // Assign if null
    //    }

    //    if (randomPlacer == null)
    //    {
    //        Debug.LogError("Load failed: RandomPlacer reference is null.");
    //        return false;
    //    }

    //    string path = Application.persistentDataPath + "/Game.Data.bat";
    //    if (File.Exists(path))
    //    {
    //        long fileSize = new FileInfo(path).Length;
    //        Debug.Log($"File Size: {fileSize} bytes");

    //        FileStream fs = File.Open(path, FileMode.Open);
    //        BinaryFormatter bf = new BinaryFormatter();
    //        if (fs.Length > 0)
    //        {
    //            randomPlacer.Uninstantiate(); // Destroy old objects before loading new ones
    //            savableObjects = (List<SavableObjects>)bf.Deserialize(fs);
    //            randomPlacer.savableObjects = savableObjects;
    //            randomPlacer.Reinstantiate();
    //            fs.Close();
    //            return true;
    //        }
    //    }
    //    return false;
    //}

    public void Save()
    {
        Get();
        savableObjects = randomPlacer.savableObjects;

        using (BinaryWriter writer = new BinaryWriter(File.Create(Application.persistentDataPath + "/Game.Data." + SlotKey + FileType)))
        {
            writer.Write(savableObjects.Count);
            foreach (var obj in savableObjects)
            {
                writer.Write(obj.id);
                writer.Write(obj.px);
                writer.Write(obj.py);
                writer.Write(obj.pz);
                writer.Write(obj.rx);
                writer.Write(obj.ry);
                writer.Write(obj.rz);
                writer.Write(obj.rw);
            }
        }
    }

    public bool Load()
    {
        if (randomPlacer == null)
        {
            randomPlacer = FindObjectOfType<RandomPlacer>(); // Assign if null
        }
        string path = Application.persistentDataPath + "/Game.Data." + SlotKey + FileType;
        if (File.Exists(path))
        {
            using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                int count = reader.ReadInt32();
                savableObjects = new List<SavableObjects>(count);

                for (int i = 0; i < count; i++)
                {
                    string id = reader.ReadString();
                    float px = reader.ReadSingle();
                    float py = reader.ReadSingle();
                    float pz = reader.ReadSingle();
                    float rx = reader.ReadSingle();
                    float ry = reader.ReadSingle();
                    float rz = reader.ReadSingle();
                    float rw = reader.ReadSingle();

                    savableObjects.Add(new SavableObjects(id, new Vector3(px, py, pz), new Quaternion(rx, ry, rz, rw)));
                }
            }

            randomPlacer.Uninstantiate();
            randomPlacer.savableObjects = savableObjects;
            randomPlacer.Reinstantiate();
            return true;
        }
        return false;
    }
}
