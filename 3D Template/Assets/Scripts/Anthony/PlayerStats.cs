using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{

    public int Level;
    public float Health;
    public float CurrentTime; // Add CurrentTime field to store the time of day

    [SerializeField] private float AutoSaveTime = 5; // Minutes
    private float CurrentAutoSaveTime = 0;

    //public void OnEnable()
    //{
    //    LoadPlayer();
    //}

    //public void Update()
    //{
    //    CurrentAutoSaveTime += Time.deltaTime / 60f; // Convert to minutes
    //    if (CurrentAutoSaveTime >= AutoSaveTime)
    //    {
    //        SavePlayer();
    //        CurrentAutoSaveTime = 0f; // Reset timer after saving
    //    }
    //}

    //public void SavePlayer()
    //{
    //    DayNightSystem dayNightSystem = Object.FindFirstObjectByType<DayNightSystem>();
    //    if (dayNightSystem != null)
    //    {
    //        Debug.Log($"DayNightSystem CurrentTime set to: {dayNightSystem.CurrentTime}");
    //        CurrentTime = dayNightSystem.CurrentTime;
    //    }

    //    SaveSystem.SaveData(this);

    //    Debug.Log("Player Data Has Been Saved");
    //}

    //public void LoadPlayer()
    //{
    //    PlayerDataStore data = SaveSystem.LoadPlayer();

    //    if (data != null)
    //    {
    //        Debug.Log($"Estimated Memory Usage: {data.GetMemorySize()} bytes");
    //        Level = data.Level;
    //        Health = data.Health;
    //        CurrentTime = data.CurrentTime; // Load the saved time

    //        Vector3 Position;
    //        Position.x = data.Position[0];
    //        Position.y = data.Position[1];
    //        Position.z = data.Position[2];
    //        transform.position = Position;

    //        // Sync the time with DayNightSystem
    //        DayNightSystem dayNightSystem = Object.FindFirstObjectByType<DayNightSystem>();
    //        if (dayNightSystem != null)
    //        {
    //            dayNightSystem.CurrentTime = CurrentTime; // Ensure the system gets the correct time
    //            Debug.Log($"Loaded Time: {CurrentTime}");
    //        }

    //        Debug.Log("Player Data Has Been Loaded     " + data.ToString());
    //    }
    //    else
    //    {
    //        Debug.Log("No saved data found. Using default values.");
    //    }

    //    SavePlayer(); // Auto-save
    //}

    //public void ResetData()
    //{
    //    SaveSystem.ResetSaveData(); // Delete the saved data
    //    Debug.Log("Player Data Has Been Reset!");
    //}

    //private void OnApplicationQuit()
    //{
    //    SavePlayer(); // Save when the player exits the game
    //}

    //public void MainMenu(string Scene)
    //{
    //    SceneManager.LoadScene(Scene);
    //    SavePlayer();
    //}
}
