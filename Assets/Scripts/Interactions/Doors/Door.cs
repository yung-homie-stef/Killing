using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PrimeTween;

public abstract class Door : Interactable
{
    [Header("Tweening")]
    [SerializeField] TweenSettings<Vector3> doorTweenSettings;

    public override void Awake()
    {
        PrimeTweenConfig.warnEndValueEqualsCurrent = false;
    }

    public override void Focus()
    {
        base.Focus();
        SetDoorOpenedTween(true);
    }

    public override void LoseFocus()
    {
        base.LoseFocus();
        SetDoorOpenedTween(false);
    }

    private void SetDoorOpenedTween(bool isOpened)
    {
        Tween.LocalRotation(this.transform, endValue: isOpened ? doorTweenSettings.startValue : doorTweenSettings.endValue, duration: 1);
    }
}
