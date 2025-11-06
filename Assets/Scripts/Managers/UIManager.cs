using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PrimeTween;

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; }
    [SerializeField] public FocusUI focusUI;
    [SerializeField] private Image _blackoutImage;
    public Dialogue dialogue { get; private set; }

    public FirstPersonController player;
    public InventoryUI inventoryUI;
    private bool _focusFlag = true;
    private bool _blackoutFlag = false;



    // Start is called before the first frame update
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
        GameEventsManager.instance.inputEvents.onQuestLogTogglePressed += EnableFocusUI;
        GameEventsManager.instance.inputEvents.onInventoryTogglePressed += EnableFocusUI;
        GameEventsManager.instance.playerEvents.onBeginPlayerTeleportation += TriggerBlackoutScreen;
        GameEventsManager.instance.playerEvents.onFinishPlayerTeleportation += TriggerBlackoutScreen;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.inputEvents.onQuestLogTogglePressed -= EnableFocusUI;
        GameEventsManager.instance.inputEvents.onInventoryTogglePressed -= EnableFocusUI;
        GameEventsManager.instance.playerEvents.onBeginPlayerTeleportation -= TriggerBlackoutScreen;
        GameEventsManager.instance.playerEvents.onFinishPlayerTeleportation -= TriggerBlackoutScreen;
    }

    private void EnableFocusUI()
    {
        _focusFlag = !_focusFlag;

        focusUI.gameObject.SetActive(_focusFlag);
    }

    private void TriggerBlackoutScreen()
    {
        _blackoutFlag = !_blackoutFlag;

        
    }

}
