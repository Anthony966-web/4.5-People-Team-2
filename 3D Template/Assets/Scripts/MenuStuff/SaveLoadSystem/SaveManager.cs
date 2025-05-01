using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; set; }

    public string saveSlot;
    private string FileType = ".bin";

    public bool IsSaveingToJson;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad (gameObject);
    }

    #region  || ---- General Section ---- ||

    #region || ----- Saving ----- ||
    public void TempSaveGame()
    {
        SaveGame();
    }

    public void SaveGame()
    {
        AllGameData data = new AllGameData();
        data.playerData = GetPlayerData();
        SavingTypeSwitch(data);
    }

    private PlayerData GetPlayerData()
    {
        float[] playerStats = new float[3];
        playerStats[0] = PlayerState.Instance.currentHealth;
        playerStats[1] = PlayerState.Instance.currentHunger;
        playerStats[2] = PlayerState.Instance.currentToxicImmunity;

        float[] playerPosAndRot = new float[7];
        playerPosAndRot[0] = PlayerState.Instance.playerBody.transform.position.x;
        playerPosAndRot[1] = PlayerState.Instance.playerBody.transform.position.y;
        playerPosAndRot[2] = PlayerState.Instance.playerBody.transform.position.z;

        playerPosAndRot[3] = PlayerState.Instance.playerBody.transform.rotation.x;
        playerPosAndRot[4] = PlayerState.Instance.playerBody.transform.rotation.y;
        playerPosAndRot[5] = PlayerState.Instance.playerBody.transform.rotation.z;
        playerPosAndRot[6] = PlayerState.Instance.playerBody.transform.rotation.w;

        return new PlayerData(playerStats, playerPosAndRot);
    }

    public void SavingTypeSwitch(AllGameData gameData)
    {
        if (IsSaveingToJson)
        {
            SaveGameDataToJsonFile(gameData);
        }
        else
        {
            SaveGameDataToBinaryFile(gameData);
        }
        
    }
    #endregion


    #region || ----- Loading ----- ||
    public AllGameData LoadingTypeSwitch()
    {
        if (IsSaveingToJson)
        {
            //AllGameData gameData = LoadGameDataFromJsonFile();
            //return gameData;
            return null;
        }
        else
        {
            AllGameData gameData = LoadGameDataFromBinaryFile();
            return gameData;
        }
    }

    public void LoadGame()
    {
        // Player Data
        SetPlayerData(LoadingTypeSwitch().playerData);

        // Enviroment Data

    }

    private void SetPlayerData(PlayerData playerData)
    {
        // Setting Player Stats

        PlayerState.Instance.currentHealth = playerData.playerStats[0];
        PlayerState.Instance.currentHunger = playerData.playerStats[1];
        PlayerState.Instance.currentToxicImmunity = playerData.playerStats[2];

        // Setting Player Position

        Vector3 loadedPosition;
        loadedPosition.x = playerData.playerPositionAndRotation[0];
        loadedPosition.y = playerData.playerPositionAndRotation[1];
        loadedPosition.z = playerData.playerPositionAndRotation[2];

        PlayerState.Instance.playerBody.transform.position = loadedPosition;

        // Setting Player Rotation

        Vector4 loadedRotation;
        loadedRotation.x = playerData.playerPositionAndRotation[3];
        loadedRotation.y = playerData.playerPositionAndRotation[4];
        loadedRotation.z = playerData.playerPositionAndRotation[5];
        loadedRotation.w = playerData.playerPositionAndRotation[6];

        PlayerState.Instance.playerBody.transform.rotation = Quaternion.Euler(loadedRotation);
    }

    public void StartLoadedGame()
    {
        SceneManager.LoadScene("Game1");

        StartCoroutine(DelayedLoading());
    }

    private IEnumerator DelayedLoading()
    {
        yield return new WaitForSeconds(1f);

        LoadGame();

        print("Game Loaded");
    }

    #endregion

    #endregion

    #region  || ---- To Binary Section ---- ||

    public void SaveGameDataToBinaryFile(AllGameData gameData)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/save_game" + saveSlot + FileType;
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, gameData);
        stream.Close();

        print("Data saved to" + Application.persistentDataPath + "/save_game" + saveSlot + FileType);
    }

    public AllGameData LoadGameDataFromBinaryFile()
    {
        string path = Application.persistentDataPath + "/save_game" + saveSlot + FileType;
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            AllGameData data = formatter.Deserialize(stream) as AllGameData;
            stream.Close();

            print("Data Loaded from" + Application.persistentDataPath + "/save_game" + saveSlot + FileType);

            return data;    
        }
        else
        {
            return null;
        }
    }


    #endregion

    #region || ---- To Json Section ---- ||

    public void SaveGameDataToJsonFile(AllGameData gameData)
    {
     
    }

    //public AllGameData LoadGameDataFromJsonFile()
    //{

    //}


    #endregion


    void OnApplicationQuit()
    {

        SaveManager.Instance.TempSaveGame();
    }

}
