using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public abstract class Interactable : MonoBehaviour
{
    public enum InteractableType
    {
        Character,
        PC,
        Item,
        Observation,
        Shop
    }

    private Sprite _sprite;
    [SerializeField] private UnityEvent _onInteract;

    [Header("Base Parameters")]
    [SerializeField] private InteractableType _type;
    public string label;

    public virtual void Awake()
    {
        // 3 being the interactable layer
        gameObject.layer = 3;

        switch (_type)
        {
            case InteractableType.Character:
                //_sprite = ;
                break;

            case InteractableType.PC:
                //_sprite = ;
                break;

            case InteractableType.Item:
                //_sprite = ;
                break;

            case InteractableType.Observation:
                //_sprite = ;
                break;
        }
    }

    // make these virtual methods in future

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
