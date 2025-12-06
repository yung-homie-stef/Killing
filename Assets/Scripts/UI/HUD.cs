using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using PrimeTween;

public class HUD : MonoBehaviour
{
    [Header("City Location Components")]
    // City Location Name is for parts of the city the player is in, or when they are inside an establishment
    [SerializeField] private TextMeshProUGUI _cityLocationText;
    [SerializeField] private RectTransform _cityLocationBanner;

    [Header("Establishment Location Components")]
    // Exterior Location Name is for when the player is outside an establishment
    [SerializeField] private TextMeshProUGUI _establishmentLocationText;
    [SerializeField] private RectTransform _establishmentLocationBanner;

    [Header("Player Funds")]
    [SerializeField] private TextMeshProUGUI _playerFundsText;
    [SerializeField] private RectTransform _playerFundsBanner;

    private CanvasGroup _HUD_CanvasGroup;
    private CanvasGroup _cityLocationBannerCanvasGroup = null;

    private void Awake()
    {
        _cityLocationBannerCanvasGroup = _cityLocationBanner.GetComponent<CanvasGroup>();
        _HUD_CanvasGroup = GetComponent<CanvasGroup>();
        _playerFundsText.text = PlayerPrefs.GetInt("Player$$$").ToString();
    }

    private void OnEnable()
    {
        GameEventsManager.instance.playerEvents.onPlayerTeleportation += HUDCityLocationTween;
        GameEventsManager.instance.playerEvents.onPlayerEnterAreaBox += UpdateHUDCityLocationInfo;
        GameEventsManager.instance.playerEvents.onBeginPlayerTeleportation += FadeOutHUD;
        GameEventsManager.instance.moneyEvents.onMoneyAmountChanged += UpdateHUDPlayerFunds;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.playerEvents.onPlayerTeleportation -= HUDCityLocationTween;
        GameEventsManager.instance.playerEvents.onPlayerEnterAreaBox -= UpdateHUDCityLocationInfo;
        GameEventsManager.instance.playerEvents.onBeginPlayerTeleportation -= FadeOutHUD;
        GameEventsManager.instance.moneyEvents.onMoneyAmountChanged -= UpdateHUDPlayerFunds;
    }

    // for functionality relating to information about the player's current locaiton on the HUD
    #region PLAYER LOCATION INFO METHODS
    private void HUDCityLocationTween()
    {
        // function for when City Location Name changes via teleporting into an establishment
        _cityLocationText.text = PlayerWorldInfo.GetCityLocationName();

        if (CityLoadManager.instance._cityLoadedIn == false)
        {
            Sequence.Create()
                .Group(Tween.Custom(startValue: 0.0f, endValue: 1.0f, duration: 1.0f, onValueChange: newVal => _cityLocationBannerCanvasGroup.alpha = newVal, startDelay: 1))
                .Group(Tween.UIAnchoredPosition(target: _cityLocationBanner, startValue: new Vector2(-690.0f, -392.0f), endValue: new Vector2(-622.0f, -392.0f), duration: 1.0f, startDelay: 1.0f));
        }
        else
            _cityLocationBannerCanvasGroup.alpha = 1.0f;
    }

    private void UpdateHUDCityLocationInfo(LocationTrigger trigger, bool flag)
    {
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
    #endregion

    private void FadeOutHUD()
    {
        Tween.Custom(startValue: 1.0f, endValue: 0.0f, duration: 1.0f, onValueChange: newVal => _cityLocationBannerCanvasGroup.alpha = newVal, startDelay: 1);
    }

    public void HUDTween(bool flag)
    {
        Sequence.Create()
            .Group(Tween.UIAnchoredPosition(target: _establishmentLocationBanner, flag ? new Vector2(0.0f, -450.0f) : new Vector2(485.0f, -450.0f), duration: 0.25f))
            .Group(Tween.UIAnchoredPosition(target: _playerFundsBanner, flag ? new Vector2(0.0f, -326.0f) : new Vector2(-460.0f, -326.0f), duration: 0.25f))
            .Group(Tween.UIAnchoredPosition(target: _cityLocationBanner, flag ? new Vector2(0.0f, -392.0f) : new Vector2(-460.0f, -392.0f), duration: 0.25f));
    }

    private void UpdateHUDPlayerFunds(int amount)
    {
        _playerFundsText.text = amount.ToString();
    }
   
}
