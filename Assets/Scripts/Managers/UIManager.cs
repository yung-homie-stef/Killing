using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PrimeTween;

public class UIManager : MonoBehaviour
{
    public Dialogue dialogue { get; private set; }
    public static UIManager instance { get; private set; }
    [SerializeField] public FocusUI focusUI;
    [SerializeField] private Image _blackoutImage;

    public InventoryUI inventoryUI;
    private Animator _blackoutAnimator = null;
    private bool _focusFlag = true;
    private bool _blackoutFlag = false;

    void Start()
    {
        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;

        focusUI = GetComponentInChildren<FocusUI>();
        dialogue = GetComponentInChildren<Dialogue>(); 
    }

    private void Awake()
    {
        GameEventsManager.instance.inputEvents.onInventoryTogglePressed += EnableFocusUI;
        GameEventsManager.instance.playerEvents.onBeginPlayerTeleportation += TriggerBlackoutScreen;
        GameEventsManager.instance.playerEvents.onPlayerTeleportation += TriggerBlackoutScreen;

        _blackoutAnimator = _blackoutImage.GetComponentInParent<Animator>();
    }

    private void OnDisable()
    {
        GameEventsManager.instance.inputEvents.onInventoryTogglePressed -= EnableFocusUI;
        GameEventsManager.instance.playerEvents.onBeginPlayerTeleportation -= TriggerBlackoutScreen;
        GameEventsManager.instance.playerEvents.onPlayerTeleportation -= TriggerBlackoutScreen;
    }

    private void EnableFocusUI()
    {
        _focusFlag = !_focusFlag;

        focusUI.gameObject.SetActive(_focusFlag);
    }

    private void TriggerBlackoutScreen()
    {
        _blackoutFlag = !_blackoutFlag;
        _blackoutAnimator.SetBool("ToBlack", _blackoutFlag);
    }

}
