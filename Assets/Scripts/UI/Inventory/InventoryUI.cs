using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.InteropServices;
using UnityEngine.EventSystems;

public class InventoryUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject _content;
    [SerializeField] private GridLayoutGroup _inventoryLayoutGroup;
    [SerializeField] private GridLayoutGroup _keyItemInventoryLayoutGroup;

    [SerializeField] private GameObject _inventoryItemButtonPrefab;
    [SerializeField] private TextMeshProUGUI _itemDescriptionText;
    [SerializeField] private TextMeshProUGUI _itemNameText;
    //[SerializeField] private UseDiscardPrompt _useDiscardPrompt;
    [HideInInspector] public Interactable targetInteractable = null;

    [Header("Buttons")]
    [SerializeField] private List<InventoryItemButton> _inventoryItemButtonList = new List<InventoryItemButton>();

    private void OnEnable()
    {
        GameEventsManager.instance.inputEvents.onInventoryTogglePressed += InventoryButtonToggle;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.inputEvents.onInventoryTogglePressed -= InventoryButtonToggle;
    }

    private void InventoryButtonToggle(bool flag)
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
    }


    public void AddItemToInventoryUI(ItemObject itemObj, InventorySlot slot)
    {
        GridLayoutGroup glg;

       if (itemObj.isKeyItem)
           glg = _keyItemInventoryLayoutGroup;
       else
           glg = _inventoryLayoutGroup;


        InventoryItemButton _inventoryItemButton = Instantiate(_inventoryItemButtonPrefab, glg.transform).GetComponent<InventoryItemButton>();
        _inventoryItemButton.Initialize(itemObj, slot);
        _inventoryItemButton.transform.SetAsFirstSibling();
        _inventoryItemButton.name = itemObj.name;
        _inventoryItemButtonList.Add(_inventoryItemButton);
    }

    public void UpdateInventoryItemDisplay(ItemObject itemObj)
    {
        _itemDescriptionText.text = itemObj.itemDescription;
        _itemNameText.text = itemObj.itemName;
    }

    public void ResetInventoryItemDisplay()
    {
        _itemDescriptionText.text = string.Empty;
    }

    public void RemoveItemFromInventoryUI(InventoryItemButton itemButton)
    {
        _inventoryItemButtonList.Remove(itemButton);
        Destroy(itemButton.gameObject);
    }
}
