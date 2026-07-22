using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PrimeTween;
using PixelCrushers.DialogueSystem;

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; }

    public FocusUI focusUI;
    [SerializeField] private Image _blackoutImage;

    /*[HideInInspector]*/ public PauseUI _pauseMenu;
    /*[HideInInspector]*/ public ShopUI _shopMenu;
    /*[HideInInspector]*/ public InventoryUI _inventoryMenu;
    /*[HideInInspector]*/ public HUD _hudMenu;
    public TransitUI _transitMenu;
    public CustomUIQuestLogWindow _questLogMenu;

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
        _pauseMenu = GetComponentInChildren<PauseUI>();
        _shopMenu = GetComponentInChildren<ShopUI>();
        _inventoryMenu = GetComponentInChildren<InventoryUI>();
        _hudMenu = GetComponentInChildren<HUD>();
        _transitMenu = GetComponentInChildren<TransitUI>();
        //_questLogMenu = (CustomUIQuestLogWindow)PixelCrushers.GameObjectUtility.FindFirstObjectByType<QuestLogWindow>();
    }

    private void OnEnable()
    {
        GameEventsManager.instance.inputEvents.onInventoryTogglePressed += EnableFocusUI;

    }

    private void OnDisable()
    {
        GameEventsManager.instance.inputEvents.onInventoryTogglePressed -= EnableFocusUI;

    }

    private void EnableFocusUI(bool flag)
    {
        focusUI.gameObject.SetActive(flag);
    }


}
