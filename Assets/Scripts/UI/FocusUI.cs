using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using PrimeTween;

public class FocusUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _interactableTypeText;
    [SerializeField] private TextMeshProUGUI _labelText;
    [SerializeField] private RectTransform _underline;

    private bool _canFocus = true;

    // Start is called before the first frame update
    void Start()
    {
        _interactableTypeText.text = string.Empty;
        _labelText.text = string.Empty;
    }

    public void UpdateFocusUI(Interactable interactable)
    {
        if (_canFocus)
        {
            _labelText.text = interactable.label;

            switch(interactable.type)
            {
                case Interactable.InteractableType.EntryDoor:
                    _interactableTypeText.text = "ENTER";
                    break;
                case Interactable.InteractableType.ExitDoor:
                    _interactableTypeText.text = "EXIT";
                    break;
                case Interactable.InteractableType.Character:
                    _interactableTypeText.text = "TALK";
                    break;
                case Interactable.InteractableType.Item:
                    _interactableTypeText.text = "PICK UP";
                    break;
            }
        }

        Tween.UISizeDelta(target: _underline, endValue: new(300.0f, 15.0f), startValue: new Vector2(0.0f,15.0f), duration: 0.25f);
    }

    public void ClearFocusUI()
    {
        _interactableTypeText.text = string.Empty;
        _labelText.text = string.Empty;
        _underline.sizeDelta = new Vector2(0.0f, 0.0f);
    }

    public void SetCanFocus(bool flag)
    {
        _canFocus = flag;
    }    
}
