using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using PrimeTween;
using PixelCrushers.DialogueSystem;
using System;
using System.Text.RegularExpressions;
using UnityEngine.Playables;

public class HUD : MonoBehaviour
{
    [Header("Player Funds")]
    [SerializeField] private TextMeshProUGUI _playerFundsText;
    [SerializeField] private RectTransform _playerFundsBanner;
    [SerializeField] private MoneyCounter _moneyCounter;
    [SerializeField] private TextMeshProUGUI _amountPopUp;
    private CanvasGroup _amountPopUpCG;

    [Header("Minimap")]
    [SerializeField] private RectTransform _minimap;

    [Header("Location")]
    [SerializeField] private RectTransform _locationBanner;
    [SerializeField] private TextMeshProUGUI _locationName;

    [Header("Alerts")]
    [SerializeField] private ItemCollectUIContainer _itemCollectUIContainer;
    [SerializeField] private Image _dialogueVisualPopupImage;

    [Header("Blackout")]
    [SerializeField] private CanvasGroup _blackoutCanvasGroup;

    private float _offScreenUIPosX = 360.0f;
    private float _onScreenUIPosX = 60.0f;
    [SerializeField] private bool _inCutscene = false;

    private void Awake()
    {
        _playerFundsText.text = PlayerPrefs.GetInt("Player$$$").ToString();
        _amountPopUpCG = _amountPopUp.GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        GameEventsManager.instance.moneyEvents.onMoneyAmountChanged += UpdateHUDPlayerFunds;
        GameEventsManager.instance.playerEvents.onBeginPlayerTeleportation += FadeToBlack;
        GameEventsManager.instance.playerEvents.onPlayerEnterIndoorOutdoor += HUDTweenIndoorOutdoor;
        GameEventsManager.instance.playerEvents.onPlayerFastTravel += FadeToWhite;
        GameEventsManager.instance.cutsceneEvents.onCutsceneBegin += HideHUDDuringCutscene;
        GameEventsManager.instance.cutsceneEvents.onCutsceneEnd += RevealHUDAfterCutscene;

        Lua.RegisterFunction("ShowDialogueVisualPopup", this, SymbolExtensions.GetMethodInfo(() => ShowDialogueVisualPopup()));
        Lua.RegisterFunction("HideDialogueVisualPopup", this, SymbolExtensions.GetMethodInfo(() => HideDialogueVisualPopup()));
    }

    private void OnDisable()
    {
        GameEventsManager.instance.moneyEvents.onMoneyAmountChanged -= UpdateHUDPlayerFunds;
        GameEventsManager.instance.playerEvents.onBeginPlayerTeleportation -= FadeToBlack;
        GameEventsManager.instance.playerEvents.onPlayerEnterIndoorOutdoor -= HUDTweenIndoorOutdoor;
        GameEventsManager.instance.playerEvents.onPlayerFastTravel -= FadeToWhite;
        GameEventsManager.instance.cutsceneEvents.onCutsceneBegin -= HideHUDDuringCutscene;
    }

    // This is to hide the minimap but move the player funds to
    // just above the banner with the name of the player location
    public void HUDTweenIndoorOutdoor(bool flag)
    {
        if (flag)
        {
            _playerFundsBanner.anchoredPosition = new Vector2(60.0f, -365.0f);
            _minimap.gameObject.SetActive(false);
        }
        else
        {
            _playerFundsBanner.anchoredPosition = new Vector2(60.0f, -10.0f); ;
            _minimap.gameObject.SetActive(true);
        }

        FadeToWhite();
    }

    // moving the HUD off the screen whenever a cutscene plays
    private void HideHUDDuringCutscene()
    {
        _inCutscene = true;

        Sequence.Create()
            .Group(Tween.UIAnchoredPosition(target: _locationBanner, startValue: _locationBanner.anchoredPosition, endValue: new Vector2(-_offScreenUIPosX, _locationBanner.anchoredPosition.y), duration: 0.25f))
            .Group(Tween.UIAnchoredPosition(target: _playerFundsBanner, startValue: _playerFundsBanner.anchoredPosition, endValue: new Vector2(-_offScreenUIPosX, _playerFundsBanner.anchoredPosition.y), duration: 0.25f))
            .Group(Tween.UIAnchoredPosition(target: _minimap, startValue: _minimap.anchoredPosition, endValue: new Vector2(-_offScreenUIPosX, _minimap.anchoredPosition.y), duration: 0.25f));
    }

    // bringing the HUD back onto the screen whenever a cutscene ends
    private void RevealHUDAfterCutscene()
    {
        _inCutscene = false;

        Sequence.Create()
           .Group(Tween.UIAnchoredPosition(target: _locationBanner, startValue: _locationBanner.anchoredPosition, endValue: new Vector2(_onScreenUIPosX, _locationBanner.anchoredPosition.y), duration: 0.25f))
           .Group(Tween.UIAnchoredPosition(target: _playerFundsBanner, startValue: _playerFundsBanner.anchoredPosition, endValue: new Vector2(_onScreenUIPosX, _playerFundsBanner.anchoredPosition.y), duration: 0.25f))
           .Group(Tween.UIAnchoredPosition(target: _minimap, startValue: _minimap.anchoredPosition, endValue: new Vector2(_onScreenUIPosX, _minimap.anchoredPosition.y), duration: 0.25f));
    }

    public void TriggerItemCollectPopup(ItemObject itemObj, bool acquired)
    {
        _itemCollectUIContainer.ShowItemPickup(itemObj, acquired);
    }

    private void UpdateHUDPlayerFunds(int previousBalance, int newBalance, int dollarAmount)
    {
        if (_inCutscene)
        {
            Sequence.Create()
                .Group(Tween.UIAnchoredPosition(target: _playerFundsBanner, startValue: _playerFundsBanner.anchoredPosition, endValue: new Vector2(_onScreenUIPosX, _playerFundsBanner.anchoredPosition.y), duration: 0.5f))
                .Chain(Tween.UIAnchoredPosition(target: _playerFundsBanner, startValue: _playerFundsBanner.anchoredPosition, endValue: new Vector2(-_offScreenUIPosX, _playerFundsBanner.anchoredPosition.y), duration: 0.5f, startDelay: 5.0f));

            StartCoroutine(PrintPlayerFunds(0.5f, dollarAmount));
        }  
        else
            StartCoroutine(PrintPlayerFunds(0.0f, dollarAmount));
   
        // tweening for "+-$$$ amount" popup above the player funds banner
        Sequence.Create().Group(Tween.TextFontSize(target: _amountPopUp, startValue: 20, endValue: 25, duration: 1.0f, startDelay: 0.5f))
                .Group(Tween.UIAnchoredPosition(target: _amountPopUp.rectTransform, startValue: new Vector2(0, 20), endValue: new Vector2(0, 45), duration: 2.0f, startDelay: 0.5f)
                .Group(Tween.Alpha(target: _amountPopUpCG, startValue: 1.0f, endValue: 0.0f, duration: 1.5f, startDelay: 0.5f)));

        _moneyCounter.UpdateBeforeCounting(previousBalance, newBalance);
    }

    private IEnumerator PrintPlayerFunds(float waitTime, int dollarAmount)
    {
        yield return new WaitForSeconds(waitTime);
        if (dollarAmount > 0)
            _amountPopUp.text = "+" + dollarAmount;
        else
            _amountPopUp.text = dollarAmount.ToString();
    }

    public void ShowDialogueVisualPopup()
    {
        // TODO: set image to specified sprite in project
        _dialogueVisualPopupImage.gameObject.SetActive(true);
        _dialogueVisualPopupImage.SetNativeSize();

        Sequence.Create()
            .Group(Tween.Custom(startValue: 0.0f, endValue: 0.5f, duration: 0.35f, onValueChange: newVal => _blackoutCanvasGroup.alpha = newVal, startDelay: 1.0f))
            .Chain(Tween.Scale(target: _dialogueVisualPopupImage.rectTransform, startValue: 0, endValue: 1, startDelay: 1, duration: .25f));
    }

    public void HideDialogueVisualPopup()
    {
        Sequence.Create()
            .Group(Tween.Scale(target: _dialogueVisualPopupImage.rectTransform, startValue: 1, endValue: 0, startDelay: 1, duration: .25f))
            .Chain(Tween.Custom(startValue: 0.5f, endValue: 0.0f, duration: 0.35f, onValueChange: newVal => _blackoutCanvasGroup.alpha = newVal, startDelay: 1.0f))
            .OnComplete(() => _dialogueVisualPopupImage.gameObject.SetActive(false));
            
    }

    #region Blackout Screen
    private void FadeToBlack()
    {
        Tween.Custom(startValue:  0.0f, endValue: 1.0f, duration: 0.35f, onValueChange: newVal => _blackoutCanvasGroup.alpha = newVal, startDelay: 0.25f);
    }

    private void FadeToWhite()
    {
        Tween.Custom(startValue: 1.0f, endValue: 0.0f, duration: 0.35f, onValueChange: newVal => _blackoutCanvasGroup.alpha = newVal, startDelay: 0.25f);
    }
    
    #endregion
}
