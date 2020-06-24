using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class containing values to save and load.
/// </summary>
[System.Serializable]
public class SaveData
{
    private static SaveData current;
    public static SaveData Current
    {
        get
        {
            if (current == null)
            {
                current = new SaveData();
            }
            return current;
        }
        set
        {
            if (value != null)
            {
                current = value;
            }
        }
    }

    public List<PlayerLevelData> playerLevelDatas;

    public SaveData()
    {
        playerLevelDatas = new List<PlayerLevelData>();
        string n = "Level01";
        playerLevelDatas.Add(new PlayerLevelData(n));
    }

    public PlayerLevelData GetPlayerLevelData(string _name)
    {
        var l = Array.Find(playerLevelDatas.ToArray(), level => level.name == _name);
        if (l == null)
        {
            l = new PlayerLevelData(_name);
            playerLevelDatas.Add(l);
        }
        return l;
    }
}

/// <summary>
/// Information about single level which needs to be saved.
/// </summary>
[System.Serializable]
public class PlayerLevelData
{
    public string name;
    public int isUnlocked;
    public int bestScore;

    public PlayerLevelData(string n)
    {
        name = n;
        isUnlocked = 1;
        bestScore = 0;
    }
}