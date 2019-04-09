using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class ConfirmPopup : BasePopup
{

    public void OnOkClick()
    {
        AudioController.Instance.PlayButtonClick();
        SceneManager.LoadScene("Menu");
    }

    public void OnCancelClick()
    {
        AudioController.Instance.PlayButtonClick();
        Hide();
    }

    public void OnRestartClick()
    {
        AudioController.Instance.PlayButtonClick();
        SceneManager.LoadScene("Gameplay");
    }

}
