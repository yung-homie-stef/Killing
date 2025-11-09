using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using PrimeTween;

public class HUD : MonoBehaviour
{
    // City Location Name is for parts of the city the player is in, or when they are inside an establishment
    [SerializeField] private TextMeshProUGUI _cityLocationText;
    [SerializeField] private RectTransform _cityLocationBanner;
    // Exterior Location Name is for when the player is outside an establishment
    [SerializeField] private TextMeshProUGUI _establishmentLocationText;
    [SerializeField] private RectTransform _establishmentLocationBanner;

    private CanvasGroup _cityLocationBannerCanvasGroup = null;
    private CanvasGroup _exteriorLocationBannerCanvasGroup = null;

    private void Awake()
    {
        _cityLocationBannerCanvasGroup = _cityLocationBanner.GetComponent<CanvasGroup>();
        _exteriorLocationBannerCanvasGroup = _establishmentLocationBanner.GetComponent<CanvasGroup>();

        GameEventsManager.instance.playerEvents.onPlayerTeleportation += HUDCityLocationTween;
        GameEventsManager.instance.playerEvents.onPlayerEnterAreaBox += UpdateHUDCityLocationInfo;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.playerEvents.onPlayerTeleportation -= HUDCityLocationTween;
        GameEventsManager.instance.playerEvents.onPlayerEnterAreaBox -= UpdateHUDCityLocationInfo;
    }

    private void HUDCityLocationTween()
    {
        // function for when City Location Name changes via teleporting into an establishment

        _cityLocationText.text = PlayerWorldInfo.GetCityLocationName();

        Sequence.Create()
            .Group(Tween.Custom(startValue: 0.0f, endValue: 1.0f, duration: 1.0f, onValueChange: newVal => _cityLocationBannerCanvasGroup.alpha = newVal, startDelay: 1))
            .Group(Tween.UIAnchoredPosition(target: _cityLocationBanner, startValue: new Vector2(-690.0f, -392.0f), endValue: new Vector2(-622.0f, -392.0f), duration: 1.0f, startDelay: 1.0f));   
    }

    private void UpdateHUDCityLocationInfo(LocationTrigger trigger, bool flag)
    {
        // overloaded function for when City Location Name changes via entering a trigger, rather than teleporting

        if (trigger.GetTriggerType() == LocationTrigger.LocationTriggerType.CityLocation)
            _cityLocationText.text = PlayerWorldInfo.GetCityLocationName();

        else if (trigger.GetTriggerType() == LocationTrigger.LocationTriggerType.EstablishmentLocation)
        {
            _establishmentLocationText.text = PlayerWorldInfo.GetEstablishmentLocationName();
            TriggerEstablishmentTween(flag);
        }
    }

    private void TriggerEstablishmentTween(bool flag)
    {
        Tween.UIAnchoredPosition(target: _establishmentLocationBanner, flag ? new Vector2(0.0f, -450.0f) : new Vector2(485.0f, -450.0f), duration: 0.25f);
    }

}
