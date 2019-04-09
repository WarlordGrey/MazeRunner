using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BasePopup : MonoBehaviour
{

    [SerializeField]
    private float animDuration = 1f;
    [SerializeField]
    private Image background = null;
    [SerializeField]
    private Image popupBack = null;

    public virtual void Show()
    {
        background.gameObject.SetActive(true);
        popupBack.gameObject.SetActive(true);
        GetComponent<CanvasGroup>().DOFade(1, animDuration);
    }

    public virtual void Hide()
    {
        GetComponent<CanvasGroup>().DOFade(0, animDuration).OnComplete(
            ()=> {
                background.gameObject.SetActive(false);
                popupBack.gameObject.SetActive(false);
            }
        );
    }

}
