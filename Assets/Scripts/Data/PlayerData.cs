using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerData
{

    private static PlayerData _instance = null;

    public int currentLevel;
    public float totalTimer;
    public float totalDistance;

    public static PlayerData Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = LoadOrCreate();
            }
            return _instance;
        }
        private set
        {
            _instance = value;
        }
    }

    private PlayerData()
    {
        currentLevel = 1;
        totalTimer = 0;
        totalDistance = 0;
    }

    public void Save()
    {
        string data = JsonUtility.ToJson(this);
        PlayerPrefs.SetString(Constants.kPrefsFilename, data);
        PlayerPrefs.Save();
    }

    private static PlayerData LoadOrCreate()
    {
        string data = PlayerPrefs.GetString(Constants.kPrefsFilename, null);
        PlayerData retVal;
        if (string.IsNullOrEmpty(data))
        {
            retVal = new PlayerData();
        }
        else
        {
            retVal = JsonUtility.FromJson<PlayerData>(data);
        }
        return retVal;
    }

}
