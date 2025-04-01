using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;
using UnityEngine.UIElements;

[System.Serializable]
public class PlayerDataStore
{
    public int Level;
    public float Health;
    public float[] Position;
    public float CurrentTime;
    //public List<BuildData> Buildings; // Store all placed buildings

    public PlayerDataStore(PlayerStats player)
    {
        Level = player.Level;
        Health = player.Health;
        CurrentTime = player.CurrentTime;

        Position = new float[3];
        Position[0] = player.transform.position.x;
        Position[1] = player.transform.position.y;
        Position[2] = player.transform.position.z;

        // Save all buildings
        //Buildings = new List<BuildData>();
        //foreach (var building in UnityEngine.Object.FindObjectsOfType<Building>())
        //{
        //    Buildings.Add(new BuildData(building));
        //}
    }

    // Default constructor for new saves
    public PlayerDataStore()
    {
        Level = 1; // Default level
        Health = 100; // Default health
        CurrentTime = 8;

        Position = new float[3] { 0, 0, 0 };

        //Buildings = new List<BuildData>(); // Initialize empty list
    }

    // Converts PlayerDataStore back into PlayerStats (for auto-saving new slots)
    //public PlayerStats ToPlayerStats()
    //{
    //    PlayerStats newPlayerSave = GameObject.Find("Player").GetComponent<PlayerStats>();
    //    if (newPlayerSave == null)
    //    {
    //        //SaveSystem.SelectedSlotId = "None";
    //        return null;
    //    }

    //    newPlayerSave.Level = Level;
    //    newPlayerSave.Health = Health;
    //    newPlayerSave.CurrentTime = CurrentTime;
    //    newPlayerSave.transform.position = new Vector3(Position[0], Position[1], Position[2]);

    //    // Restore all buildings
    //    foreach (var buildData in Buildings)
    //    {
    //        GameObject newBuilding = GameObject.Instantiate(BuildingManager.Instance.BuildPrefab);
    //        newBuilding.transform.position = new Vector3(buildData.Position[0], buildData.Position[1], buildData.Position[2]);
    //        newBuilding.transform.eulerAngles = new Vector3(buildData.Rotation[0], buildData.Rotation[1], buildData.Rotation[2]);

    //        // Ensure the new object has a Building component
    //        Building buildingComponent = newBuilding.GetComponent<Building>();
    //        if (buildingComponent == null)
    //        {
    //            buildingComponent = newBuilding.AddComponent<Building>();
    //        }

    //        buildingComponent.ID = buildData.ID;
    //    }

    //    return newPlayerSave;
    //}

    // Calculates memory size
    public long GetMemorySize()
    {
        long before = GC.GetTotalMemory(true);
        object temp = this;
        long after = GC.GetTotalMemory(true);
        return after - before;
    }

    // Print for debugging
    public override string ToString()
    {
        //string buildInfo = $"Buildings Count: {Buildings.Count}";
        return $"PlayerDataStore: Level={Level}, Health={Health}, Position=({Position[0]}, {Position[1]}, {Position[2]}), Time={CurrentTime}";
    }

    //[System.Serializable]
    //public class BuildData
    //{
    //    public int ID;
    //    public float[] Position;
    //    public float[] Rotation;

    //    public BuildData(Building building)
    //    {
    //        ID = building.ID;
    //        Position = new float[] { building.transform.position.x, building.transform.position.y, building.transform.position.z };
    //        Rotation = new float[] { building.transform.eulerAngles.x, building.transform.eulerAngles.y, building.transform.eulerAngles.z };
    //    }
    //}
}