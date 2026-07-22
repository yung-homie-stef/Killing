using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using PrimeTween;
using PixelCrushers.DialogueSystem;
using System;
using System.Text.RegularExpressions;

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
    [SerializeField] private TextMeshProUGUI _locationName;

    [Header("Alerts")]
    [SerializeField] private ItemCollectUIContainer _itemCollectUIContainer;
    [SerializeField] private Image _dialogueVisualPopupImage;

    [Header("Blackout")]
    [SerializeField] private CanvasGroup _blackoutCanvasGroup;

    private void Awake()
    {
        _playerFundsText.text = PlayerPrefs.GetInt("Player$$$").ToString();
        _amountPopUpCG = _amountPopUp.GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        GameEventsManager.instance.moneyEvents.onMoneyAmountChanged += UpdateHUDPlayerFunds;
        GameEventsManager.instance.playerEvents.onBeginPlayerTeleportation += FadeToBlack;
        GameEventsManager.instance.playerEvents.onPlayerEnterIndoorOutdoor += HUDTween;
        GameEventsManager.instance.playerEvents.onPlayerFastTravel += HUDTween;

        Lua.RegisterFunction("ShowDialogueVisualPopup", this, SymbolExtensions.GetMethodInfo(() => ShowDialogueVisualPopup()));
        Lua.RegisterFunction("HideDialogueVisualPopup", this, SymbolExtensions.GetMethodInfo(() => HideDialogueVisualPopup()));
    }

    private void OnDisable()
    {
        GameEventsManager.instance.moneyEvents.onMoneyAmountChanged -= UpdateHUDPlayerFunds;
        GameEventsManager.instance.playerEvents.onBeginPlayerTeleportation -= FadeToBlack;
        GameEventsManager.instance.playerEvents.onPlayerEnterIndoorOutdoor -= HUDTween;
        GameEventsManager.instance.playerEvents.onPlayerFastTravel -= HUDTween;
    }

    public void HUDTween(bool flag)
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

    public void HUDTween()
    {
        _playerFundsBanner.anchoredPosition = new Vector2(60.0f, -10.0f); ;
        _minimap.gameObject.SetActive(true);

        FadeToWhite();
    }

    public void TriggerItemCollectPopup(ItemObject itemObj, bool acquired)
    {
        _itemCollectUIContainer.ShowItemPickup(itemObj, acquired);
    }

    private void UpdateHUDPlayerFunds(int previousBalance, int newBalance, int dollarAmount)
    {
        if (dollarAmount > 0)
            _amountPopUp.text = "+" + dollarAmount;
        else
            _amountPopUp.text = dollarAmount.ToString();

            Sequence.Create().Group(Tween.TextFontSize(target: _amountPopUp, startValue: 20, endValue: 25, duration: 1.0f))
                .Group(Tween.UIAnchoredPosition(target: _amountPopUp.rectTransform, startValue: new Vector2(0, 20), endValue: new Vector2(0, 45), duration: 2.0f)
                .Group(Tween.Alpha(target: _amountPopUpCG, startValue: 1.0f, endValue: 0.0f, duration: 1.5f)));

        _moneyCounter.UpdateBeforeCounting(previousBalance, newBalance);
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

    private void FadeToBlack()
    {
        Tween.Custom(startValue:  0.0f, endValue: 1.0f, duration: 0.35f, onValueChange: newVal => _blackoutCanvasGroup.alpha = newVal, startDelay: 0.25f);
    }

    private void FadeToWhite()
    {
        Tween.Custom(startValue: 1.0f, endValue: 0.0f, duration: 0.35f, onValueChange: newVal => _blackoutCanvasGroup.alpha = newVal, startDelay: 0.25f);
    }

}
