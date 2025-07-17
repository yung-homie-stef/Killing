using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.Events;

public class InventoryItemButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{

    [SerializeField] private TextMeshProUGUI _itemName;
    private ItemObject _itemObject;
    private Button _button;
    [SerializeField] private GameObject _usePrompt;
    [SerializeField] private GameObject _discardPrompt;
    [SerializeField] private InventorySlot _inventorySlot;

    // Start is called before the first frame update
    void Start()
    {
        _button = GetComponent<Button>();
    }

    public void Initialize(ItemObject itemObj, InventorySlot slot)
    {
        _itemObject = itemObj;
        _itemName.text = itemObj.itemName;
        _inventorySlot = slot;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //UIManager.instance.inventoryUI.UpdateInventoryItemDisplay();
        Debug.Log("selected " + _itemObject.itemName);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            PromptItemUse(_usePrompt);
        else if (eventData.button == PointerEventData.InputButton.Right)
            PromptItemUse(_discardPrompt);
        
    }

    private void PromptItemUse(GameObject obj)
    {
        _button.Select();
        _button.interactable = false;
        _itemName.gameObject.SetActive(false);
        obj.SetActive(true);
    }

    public void ExitPrompt()
    {
        _button.interactable = true;
        _usePrompt.gameObject.SetActive(false);
        _discardPrompt.gameObject.SetActive(false);
        _itemName.gameObject.SetActive(true);
    }

    public void DiscardItem()
    {
        UIManager.instance.inventoryUI.RemoveItemFromInventoryUI(this);
        InventoryManager.instance._itemInventory.RemoveItem(_inventorySlot);
    }

}
