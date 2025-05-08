using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;
using UnityEngine.UIElements;
using Unity.VisualScripting;

[System.Serializable]
public class PlayerData
{
    public float[] playerStats; // [0] - Health, [1] - 

    public float[] playerPositionAndRotation;

    public ItemAssets[] inventoryContent;

    public ItemAssets[] quickSlotsContent;

    //public string[] inventoryContent;

    public PlayerData(float[] _playerStats, float[] _playerPosAndRot, ItemAssets[] _inventoryContent, ItemAssets[] _quickSlotsContent)
    {
        playerStats = _playerStats;
        playerPositionAndRotation = _playerPosAndRot;
        inventoryContent = _inventoryContent;
        quickSlotsContent = _quickSlotsContent;
    }


}