using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YouLostPopup : BasePopup
{

    public void OnRestartClick()
    {
        AudioController.Instance.PlayButtonClick();
        SceneManager.LoadScene("Gameplay");
    }

    public void OnExitClick()
    {
        AudioController.Instance.PlayButtonClick();
        SceneManager.LoadScene("Menu");
    }

}
