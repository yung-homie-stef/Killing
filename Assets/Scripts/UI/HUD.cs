using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using PrimeTween;

public class HUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _locationNameText;
    [SerializeField] private RectTransform _locationBanner;
    private CanvasGroup _locationBannerCanvasGroup = null;

    private void Awake()
    {
        _locationBannerCanvasGroup = _locationBanner.GetComponent<CanvasGroup>();
        GameEventsManager.instance.playerEvents.onPlayerTeleportation += UpdateHUDLocationInfo;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.playerEvents.onPlayerTeleportation -= UpdateHUDLocationInfo;
    }

    private void UpdateHUDLocationInfo()
    {
        _locationNameText.text = PlayerWorldInfo.GetLocationName();

        Sequence.Create()
            .Group(Tween.Custom(startValue: 0.0f, endValue: 1.0f, duration: 1.0f, onValueChange: newVal => _locationBannerCanvasGroup.alpha = newVal, startDelay: 1))
            .Group(Tween.UIAnchoredPosition(target: _locationBanner, startValue: new Vector2(-660.0f, -392.0f), endValue: new Vector2(-622.0f, -392.0f), duration: 1.0f, startDelay: 1.0f)); 
        
    }
}
