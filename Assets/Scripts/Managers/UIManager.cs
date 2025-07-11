using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; }
    [SerializeField] public FocusUI focusUI;
    public Dialogue dialogue { get; private set; }

    public FirstPersonController player;
    public InventoryUI inventoryUI;
    private bool _focusFlag = true;



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
    }

    private void OnDisable()
    {
        GameEventsManager.instance.inputEvents.onQuestLogTogglePressed -= EnableFocusUI;
    }

    private void EnableFocusUI()
    {
        _focusFlag = !_focusFlag;

        focusUI.gameObject.SetActive(_focusFlag);
    }

}
