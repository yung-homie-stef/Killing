using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.Events;

public class InventoryItemButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [HideInInspector] public ItemObject _itemObject;
    [SerializeField] private TextMeshProUGUI _itemName;
    [SerializeField] private Button _useButton;
    [SerializeField] private Button _discardButton;
    [SerializeField] private InventorySlot _inventorySlot;

    private Button _button;
    private UIManager _UIManager = null;
    private InventoryManager _inventoryManager = null;

    // Start is called before the first frame update
    void Start()
    {
        _button = GetComponent<Button>();

        if (UIManager.instance != null ) 
            _UIManager = UIManager.instance;

        if (InventoryManager.instance != null )
            _inventoryManager = InventoryManager.instance;
    }

    public void Initialize(ItemObject itemObj, InventorySlot slot)
    {
        _itemObject = itemObj;
        _itemName.text = itemObj.itemName;
        _inventorySlot = slot;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _UIManager.inventoryUI.UpdateInventoryItemDisplay(_itemObject);

        if (_UIManager.inventoryUI.targetInteractable == null)
        {
            _useButton.gameObject.SetActive(true);
            _discardButton.gameObject.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _useButton.gameObject.SetActive(false);
        _discardButton.gameObject.SetActive(false);
    }

    public void InventoryItemButtonPress(bool isUsing)
    {
        if (_UIManager.inventoryUI.targetInteractable == null)
            _UIManager.inventoryUI.PromptItemUseOrDiscard(this, _inventorySlot, isUsing);
    }

    public void DiscardItem()
    {
        _UIManager.inventoryUI.RemoveItemFromInventoryUI(this);
        _inventoryManager._itemInventory.RemoveItem(_inventorySlot);
    }

}
