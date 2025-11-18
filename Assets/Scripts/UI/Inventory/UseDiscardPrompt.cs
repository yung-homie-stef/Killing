using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UseDiscardPrompt : MonoBehaviour
{
    [Header("Components")]
    private GameObject _content;
    [SerializeField] private TextMeshProUGUI _yesButtonText;
    [SerializeField] private TextMeshProUGUI _promptText;
    private InventorySlot _currentInventorySlot;
    private InventoryItemButton _currentInventoryButton;
    private Mode _itemMode = Mode.None;

    private void Awake()
    {
        _content = transform.GetChild(0).gameObject;
    }

    private enum Mode
    {
        Using,
        Discarding,
        None
    }

    public void InitializePrompt(InventoryItemButton btn, InventorySlot slot, bool flag)
    {
        _content.SetActive(true);
        _currentInventoryButton = btn;
        _currentInventorySlot = slot;

        switch (flag)
        {
            case true:
                _yesButtonText.text = "Use";
                _promptText.text = ("Use " + btn._itemObject.itemName + "?");
                _itemMode = Mode.Using;
                break;
            case false:
                _yesButtonText.text = "Discard";
                _promptText.text = ("Discard " + btn._itemObject.itemName + "?");
                _itemMode = Mode.Discarding;
                break;
        }
    }

    public void CancelPrompt()
    {
        _content.SetActive(false);
        _currentInventoryButton = null;
        _currentInventorySlot = null;
        _itemMode = Mode.None;

        UIManager.instance.inventoryUI.CloseItemUseOrDiscard();
        _content.SetActive(false);
    }

    public void YesToPrompt()
    {
        switch (_itemMode)
        {
            case Mode.Using:
                break;
            case Mode.Discarding:
                if (_currentInventoryButton != null && _currentInventorySlot != null)
                    _currentInventoryButton.DiscardItem();
                break;
        }

        UIManager.instance.inventoryUI.CloseItemUseOrDiscard();
        _content.SetActive(false);
    }
}
