using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.InteropServices;
using UnityEngine.EventSystems;
using PixelCrushers.DialogueSystem;

public class InventoryUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject _content;
    [SerializeField] private GridLayoutGroup _inventoryLayoutGroup;
    [SerializeField] private GridLayoutGroup _keyItemInventoryLayoutGroup;
    [SerializeField] private GameObject _inventoryItemButtonPrefab;
    [SerializeField] private GameObject _keyItemHierarchy;
    [SerializeField] private GameObject _standardItemHierarchy;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI _itemDescriptionText;
    [SerializeField] private TextMeshProUGUI _itemNameText;
    //[SerializeField] private UseDiscardPrompt _useDiscardPrompt;
    [HideInInspector] public Interactable targetInteractable = null;
    [SerializeField] private TextMeshProUGUI _keyItemButtonText;
    [SerializeField] private TextMeshProUGUI _standardItemButtonText;

    [Header("Buttons Lists")]
    [SerializeField] private List<InventoryItemButton> _inventoryItemButtonList = new List<InventoryItemButton>();
    [SerializeField] private List<InventoryItemButton> _keyItemButtonList = new List<InventoryItemButton>();
    [SerializeField] private Button _closeButton;

    [Header("Requested Item Type")]
    [SerializeField] public ItemType _requestedItemType = ItemType.None;
    private bool _gaveCorrectItem = false;
    private bool _exitedInventoryPrompt = false;

    private void OnEnable()
    {
        GameEventsManager.instance.inputEvents.onInventoryTogglePressed += InventoryToggle;
        Lua.RegisterFunction("InventoryToggleFromDialogue", this, SymbolExtensions.GetMethodInfo(() => InventoryToggleFromDialogue(string.Empty)));
        Lua.RegisterFunction("CheckForKeyItem", this, SymbolExtensions.GetMethodInfo(() => CheckForKeyItem(string.Empty, false)));
        Lua.RegisterFunction("CheckIfCorrectItemWasGiven", this, SymbolExtensions.GetMethodInfo(() => CheckIfCorrectItemWasGiven()));
        Lua.RegisterFunction("CheckIfInventoryPromptWasExited", this, SymbolExtensions.GetMethodInfo(() => CheckIfInventoryPromptWasExited()));
    }

    private void OnDisable()
    {
        GameEventsManager.instance.inputEvents.onInventoryTogglePressed -= InventoryToggle;
    }

    private void InventoryToggle(bool flag)
    {
        if (flag)
        {
            ShowOrHideInventoryUI(true, CursorLockMode.None);
            GameEventsManager.instance.playerEvents.DisablePlayerMovement();
        }
        else
        {
            ShowOrHideInventoryUI(false, CursorLockMode.Locked);
            GameEventsManager.instance.playerEvents.EnablePlayerMovement();
        }
    }

    private void ShowOrHideInventoryUI(bool flag, CursorLockMode mode)
    {
        Cursor.lockState = mode;
        Cursor.visible = flag;
        _content.SetActive(flag);


        if (flag)
            CheckIfThereAreAnyItemsToFillDescription(_inventoryItemButtonList);
    }

    public void SwitchItemTab(bool flag)
    {
        if (flag) // switching to key items
        {
            _keyItemHierarchy.SetActive(true);
            _standardItemHierarchy.SetActive(false);
            CheckIfThereAreAnyItemsToFillDescription(_keyItemButtonList);
            _keyItemButtonText.color = Color.black;
            _standardItemButtonText.color = Color.white;
        }
        else
        {
            _keyItemHierarchy.SetActive(false);
            _standardItemHierarchy.SetActive(true);
            CheckIfThereAreAnyItemsToFillDescription(_inventoryItemButtonList);
            _keyItemButtonText.color = Color.white;
            _standardItemButtonText.color = Color.black;
        }
    }

    public void AddItemToInventoryUI(ItemObject itemObj, InventorySlot slot)
    {
        GridLayoutGroup glg;
        List<InventoryItemButton> liib;


        if (itemObj.isKeyItem)
        {
            glg = _keyItemInventoryLayoutGroup;
            liib = _keyItemButtonList;
        }
        else
        {
            glg = _inventoryLayoutGroup;
            liib = _inventoryItemButtonList;
        }

        InventoryItemButton _inventoryItemButton = Instantiate(_inventoryItemButtonPrefab, glg.transform).GetComponent<InventoryItemButton>();
        _inventoryItemButton.Initialize(itemObj, slot);
        _inventoryItemButton.transform.SetAsFirstSibling();
        _inventoryItemButton.name = itemObj.name;
        liib.Add(_inventoryItemButton);
    }

    public void UpdateInventoryItemDisplay(ItemObject itemObj)
    {
        _itemDescriptionText.text = itemObj.itemDescription;
        _itemNameText.text = " " + itemObj.itemName;
    }

    public void ResetInventoryItemDisplay()
    {
        _itemDescriptionText.text = string.Empty;
        _itemNameText.text = string.Empty;
    }

    public void RemoveItemFromInventoryUI(InventoryItemButton itemButton)
    {
        if (itemButton._itemObject.isKeyItem)
            _keyItemButtonList.Remove(itemButton);
        else
            _inventoryItemButtonList.Remove(itemButton);

        Destroy(itemButton.gameObject);
    }

    // Done when inventory is first opened up so text box doesn't start off empty
    private void CheckIfThereAreAnyItemsToFillDescription(List<InventoryItemButton> list)
    {
        if (list.Count > 0)
            UpdateInventoryItemDisplay(list[^1]._itemObject);
        else
            ResetInventoryItemDisplay();
    }

    #region NPC Related Item Trade-offs

    public void InventoryToggleFromDialogue(string type)
    {
        _requestedItemType = (ItemType)System.Enum.Parse(typeof(ItemType), type);
        _gaveCorrectItem = false;
        GameEventsManager.instance.inputEvents.SetInventoryFlag(true);
        InventoryToggle(true);
        _exitedInventoryPrompt = false;
    }

    public bool CheckForKeyItem(string name, bool remove)
    {
        for (int i = 0; i < _keyItemButtonList.Count; i++)
        {
            if (_keyItemButtonList[i]._itemObject.itemName == name)
            {
                if (remove)
                {
                    RemoveItemFromInventoryUI(_keyItemButtonList[i]);
                    return true;
                }
                return true;
            }
        }
        return false;
    }

    public void CloseInventory()
    {
        if (_requestedItemType == ItemType.None)
            InventoryToggle(false);
        else
        {
            ShowOrHideInventoryUI(false, CursorLockMode.None);
            _exitedInventoryPrompt = true;
            // item is not selected however this signal is just used to continue the conversation
            PixelCrushers.DialogueSystem.Sequencer.Message("ItemSelected");
            GameEventsManager.instance.inputEvents.SetInventoryFlag(false);
        }
    }
    public void CheckGivenItem(InventoryItemButton itemButton)
    {
        if (itemButton._itemObject.itemType == _requestedItemType)
        {
            _gaveCorrectItem = true;
            RemoveItemFromInventoryUI(itemButton);
        }
        else if (itemButton._itemObject.itemType != _requestedItemType)
            _gaveCorrectItem = false;


        _requestedItemType = ItemType.None;
        ShowOrHideInventoryUI(false, CursorLockMode.None);
        PixelCrushers.DialogueSystem.Sequencer.Message("ItemSelected");
    }

    public bool CheckIfCorrectItemWasGiven()
    {
        return _gaveCorrectItem;
    }

    public bool CheckIfInventoryPromptWasExited()
    {
        return _exitedInventoryPrompt;
    }
    #endregion
}
