using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance { get; private set; } = null;

    [SerializeField]
    private ConfirmPopup confirmPopup = null;
    [SerializeField]
    private CongratsPopup congratsPopup = null;
    [SerializeField]
    private YouLostPopup youLostPopup = null;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    public void OnExitClick()
    {
        AudioController.Instance.PlayButtonClick();
        confirmPopup.Show();
    }

    public void ShowCongrats()
    {
        congratsPopup.Show();
    }

    public void ShowYouLost()
    {
        youLostPopup.Show();
    }

}
