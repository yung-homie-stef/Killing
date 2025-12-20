using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PrimeTween;
using PixelCrushers.DialogueSystem;

public class UIManager : MonoBehaviour
{
    public Dialogue dialogue { get; private set; }
    public static UIManager instance { get; private set; }

    [HideInInspector] public FocusUI focusUI;
    [SerializeField] private Image _blackoutImage;

    [HideInInspector] public PauseUI _pauseMenu;
    [HideInInspector] public ShopUI _shopMenu;
    [HideInInspector] public InventoryUI _inventoryMenu;
    [HideInInspector] public HUD _hudMenu;

    private Animator _blackoutAnimator = null;
    private bool _blackoutFlag = false;

    void Start()
    {
        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;
    }

    private void Awake()
    {
        focusUI = GetComponentInChildren<FocusUI>();
        dialogue = GetComponentInChildren<Dialogue>();
        _pauseMenu = GetComponentInChildren<PauseUI>();
        _shopMenu = GetComponentInChildren<ShopUI>();
        _inventoryMenu = GetComponentInChildren<InventoryUI>();
        _hudMenu = GetComponentInChildren<HUD>();

        _blackoutAnimator = _blackoutImage.GetComponentInParent<Animator>();
    }

    private void OnEnable()
    {
        GameEventsManager.instance.inputEvents.onInventoryTogglePressed += EnableFocusUI;
        GameEventsManager.instance.playerEvents.onBeginPlayerTeleportation += TriggerBlackoutScreen;
        GameEventsManager.instance.playerEvents.onPlayerTeleportation += TriggerBlackoutScreen;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.inputEvents.onInventoryTogglePressed -= EnableFocusUI;
        GameEventsManager.instance.playerEvents.onBeginPlayerTeleportation -= TriggerBlackoutScreen;
        GameEventsManager.instance.playerEvents.onPlayerTeleportation -= TriggerBlackoutScreen;
    }

    private void EnableFocusUI(bool flag)
    {
        focusUI.gameObject.SetActive(flag);
    }

    private void TriggerBlackoutScreen()
    {
        _blackoutFlag = !_blackoutFlag;
        _blackoutAnimator.SetBool("ToBlack", _blackoutFlag);
    }

}
