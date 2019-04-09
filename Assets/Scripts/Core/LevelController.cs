using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController
{
    private static LevelController _instance = null;

    private float timer = 0;
    private float distance = 0;

    public static LevelController Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new LevelController();
            }
            return _instance;
        }
        private set
        {
            _instance = value;
        }
    }

    private LevelController()
    {
        CanPlayerMove = true;
    }

    public void Resfresh()
    {
        timer = 0;
        distance = 0;
        CanPlayerMove = true;
    }

    public void DoWin()
    {
        CanPlayerMove = false;
        AudioController.Instance.PlayWin();
        PlayerData.Instance.totalTimer += timer;
        PlayerData.Instance.totalDistance += distance;
        PlayerData.Instance.currentLevel++;
        PlayerData.Instance.Save();
        UIManager.Instance.ShowCongrats();
    }

    public void DoLost()
    {
        CanPlayerMove = false;
        UIManager.Instance.ShowYouLost();
    }

    public bool CanPlayerMove
    {
        get; set;
    }

    public float Distance
    {
        get
        {
            return distance;
        }
        set
        {
            if (CanPlayerMove)
            {
                distance = value;
            }
        }
    }

    public float Timer
    {
        get
        {
            return timer;
        }
        set
        {
            if (CanPlayerMove)
            {
                timer = value;
            }
        }
    }

    public float LevelWidth
    {
        get
        {
            return PlayerData.Instance.currentLevel / 2 + 40;
        }
    }

    public float LevelLength
    {
        get
        {
            return PlayerData.Instance.currentLevel / 2 + 39 + ((PlayerData.Instance.currentLevel % 2) == 1 ? 1 : 0);
        }
    }

    public int EntrancesCount
    {
        get
        {
            int ind = PlayerData.Instance.currentLevel - 1;
            ind = ind >= Constants.kEntrancesPerLevels.Length ? Constants.kEntrancesPerLevels.Length - 1 : ind;
            return Constants.kEntrancesPerLevels[ind];
        }
    }

    public int TrapsCount
    {
        get
        {
            return PlayerData.Instance.currentLevel / Constants.kTrapsIncreaseLevel + Constants.kInitialTrapsCount;
        }
    }

}
