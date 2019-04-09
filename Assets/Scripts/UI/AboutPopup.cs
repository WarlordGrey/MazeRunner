using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AboutPopup : BasePopup
{
    public void OnOkClick()
    {
        AudioController.Instance.PlayButtonClick();
        Hide();
    }

    public override void Show()
    {
        base.Show();
        AudioController.Instance.PlayButtonClick();
    }

}
