using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using PrimeTween;
using PixelCrushers.DialogueSystem;
using System;

public class HUD : MonoBehaviour
{

    [Header("Player Funds")]
    [SerializeField] private TextMeshProUGUI _playerFundsText;
    [SerializeField] private RectTransform _playerFundsBanner;
    [SerializeField] private MoneyCounter _moneyCounter;

    [Header("Alerts")]
    [SerializeField] private ItemCollectUIContainer _itemCollectUIContainer;
    [SerializeField] private Image _dialogueVisualPopupImage;

    [Header("Blackout")]
    [SerializeField] private CanvasGroup _blackoutCanvasGroup;


    // UI Position Variables (to avoid magic numbers)
    private Vector2 _establishmentPos = Vector2.zero;
    private float locationWidth = 0.0f;
    private float fundsWidth = 0.0f;
    private float establishmentWidth = 0.0f;

    private void Awake()
    {
        _playerFundsText.text = PlayerPrefs.GetInt("Player$$$").ToString();
        fundsWidth = _playerFundsBanner.rect.width;
    }

    private void OnEnable()
    {
        GameEventsManager.instance.playerEvents.onBeginPlayerTeleportation += FadeOutHUD;
        GameEventsManager.instance.moneyEvents.onMoneyAmountChanged += UpdateHUDPlayerFunds;

        Lua.RegisterFunction("ShowDialogueVisualPopup", this, SymbolExtensions.GetMethodInfo(() => ShowDialogueVisualPopup()));
        Lua.RegisterFunction("HideDialogueVisualPopup", this, SymbolExtensions.GetMethodInfo(() => HideDialogueVisualPopup()));
        //DialogueManager.instance.conversationStarted += TriggerHideHUDTween;
        //DialogueManager.instance.conversationEnded += TriggerShowHUDTween;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.playerEvents.onBeginPlayerTeleportation -= FadeOutHUD;
        GameEventsManager.instance.moneyEvents.onMoneyAmountChanged -= UpdateHUDPlayerFunds;
        //DialogueManager.instance.conversationStarted -= TriggerHideHUDTween;
    }

    private void FadeOutHUD()
    {
        
    }

    public void HUDTween(bool flag)
    {
        Sequence.Create()
            .Group(Tween.UIAnchoredPosition(target: _playerFundsBanner, flag ? new Vector2(60.0f, -105.0f) : new Vector2(-fundsWidth, -105.0f), duration: 0.25f));
    }

    public void TriggerItemCollectPopup(ItemObject itemObj)
    {
        _itemCollectUIContainer.ShowItemPickup(itemObj);
    }

    private void UpdateHUDPlayerFunds(int previousAmount, int amount)
    {
        //_playerFundsText.text = amount.ToString();
        _moneyCounter.UpdateBeforeCounting(previousAmount, amount);
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


    private void TriggerHideHUDTween(Transform t)
    {
        HUDTween(false);
    }
    private void TriggerShowHUDTween(Transform t)
    {
        HUDTween(true);
    }
}
