using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;
using PrimeTween;

public class PauseButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        TweenPauseButton(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TweenPauseButton(false);
    }

    private void TweenPauseButton(bool flag)
    {
        Tween.Scale(target: this.transform, endValue: flag ? 2 : 1, duration: 0.25f);
    }

}
