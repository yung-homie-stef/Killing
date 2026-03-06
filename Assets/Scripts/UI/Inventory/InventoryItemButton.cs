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
    [SerializeField] private InventorySlot _inventorySlot;
    [SerializeField] private Image _itemImage;

    private UIManager _UIManager = null;
    private InventoryManager _inventoryManager = null;

    // Start is called before the first frame update
    void Start()
    {
        if (UIManager.instance != null ) 
            _UIManager = UIManager.instance;

        if (InventoryManager.instance != null )
            _inventoryManager = InventoryManager.instance;
    }

    public void Initialize(ItemObject itemObj, InventorySlot slot)
    {
        _itemObject = itemObj;
        _inventorySlot = slot;
        _itemImage.sprite = itemObj.itemIcon;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _UIManager._inventoryMenu.UpdateInventoryItemDisplay(_itemObject);
    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }

    public void DiscardItem()
    {
        _UIManager._inventoryMenu.RemoveItemFromInventoryUI(this);
        _inventoryManager._itemInventory.RemoveItem(_inventorySlot);
    }

}
