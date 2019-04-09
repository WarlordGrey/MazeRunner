using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CongratsPopup : BasePopup
{
    
    [SerializeField]
    private Text timerText = null;
    [SerializeField]
    private Text distanceText = null;

    public void OnOkClick()
    {
        AudioController.Instance.PlayButtonClick();
        SceneManager.LoadScene("Gameplay");
    }

    public void OnCancelClick()
    {
        AudioController.Instance.PlayButtonClick();
        SceneManager.LoadScene("Menu");
    }

    public override void Show()
    {
        base.Show();
        timerText.text = string.Format("Time: {0}s", Mathf.RoundToInt(LevelController.Instance.Timer));
        distanceText.text = string.Format("Distance: {0}", Mathf.RoundToInt(LevelController.Instance.Distance));
    }

}
