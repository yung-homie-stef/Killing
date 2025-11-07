using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public abstract class Interactable : MonoBehaviour
{
    [Header("Base Parameters")]
    public string label;
    [Space(15)]
    [SerializeField] private UnityEvent _onInteract;

    public virtual void Awake()
    {
        // 3 being the interactable layer
        gameObject.layer = 3;
    }

    public virtual void Interact()
    {
        UIManager.instance.focusUI.SetCanFocus(false);
        UIManager.instance.focusUI.ClearFocusUI();
        _onInteract.Invoke();
    }

    public virtual void Focus()
    {
        UIManager.instance.focusUI.UpdateFocusUI(this);
    }
    public virtual void LoseFocus()
    {
        UIManager.instance.focusUI.ClearFocusUI();
    }
}
