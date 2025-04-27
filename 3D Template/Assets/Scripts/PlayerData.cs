using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;
using UnityEngine.UIElements;

[System.Serializable]
public class PlayerData
{
    public float[] playerStats; // [0] - Health, [1] - 

    public float[] playerPositionAndRotation;

    //public string[] inventoryContent;

    public PlayerData(float[] _playerStats, float[] _playerPosAndRot)
    {
        playerStats = _playerStats;
        playerPositionAndRotation = _playerPosAndRot;

    }
}