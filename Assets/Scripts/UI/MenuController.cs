using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{

    [SerializeField]
    private Text totalDistanceText =null;
    [SerializeField]
    private Text totalTimerText = null;
    [SerializeField]
    private Text currentLevelText = null;

    private void Start()
    {
        totalDistanceText.text = string.Format("{0}", Mathf.RoundToInt(PlayerData.Instance.totalDistance));
        totalTimerText.text = GetFormattedTimeFromSeconds(PlayerData.Instance.totalTimer);
        currentLevelText.text = string.Format("{0}", PlayerData.Instance.currentLevel);
    }

    public void OnPlayClick()
    {
        AudioController.Instance.PlayButtonClick();
        SceneManager.LoadScene("Gameplay");
    }

    public void OnExitClick()
    {
        AudioController.Instance.PlayButtonClick();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private string GetFormattedTimeFromSeconds(float seconds)
    {
        TimeSpan time = TimeSpan.FromSeconds(seconds);
        return time.ToString("hh':'mm':'ss");
    }

}
