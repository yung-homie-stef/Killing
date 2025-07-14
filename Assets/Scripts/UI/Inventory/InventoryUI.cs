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
    [SerializeField] private GameObject _contentParent;
    [SerializeField] private VerticalLayoutGroup _inventoryLayoutGroup;
    [SerializeField] private VerticalLayoutGroup _keyItemInventoryLayoutGroup;
    [SerializeField] private GameObject _inventoryItemButtonPrefab;
    [SerializeField] private List<InventoryItemButton> _inventoryItemButtonList = new List<InventoryItemButton>();

    private void OnEnable()
    {
        GameEventsManager.instance.inputEvents.onInventoryToggledPressed += InventoryButtonToggle;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.inputEvents.onInventoryToggledPressed -= InventoryButtonToggle;
    }

    public void AddItemToInventoryUI(ItemObject itemObj)
    {
       VerticalLayoutGroup vlg;

       if (itemObj.isKeyItem)
           vlg = _keyItemInventoryLayoutGroup;
       else
           vlg = _inventoryLayoutGroup;


        InventoryItemButton _inventoryItemButton = Instantiate(_inventoryItemButtonPrefab, vlg.transform).GetComponent<InventoryItemButton>();
        _inventoryItemButton.Initialize(itemObj);
        _inventoryItemButton.transform.SetAsFirstSibling();
        _inventoryItemButton.name = itemObj.name;
        _inventoryItemButtonList.Add(_inventoryItemButton);
    }

    private void InventoryButtonToggle()
    {
        if (_contentParent.activeInHierarchy)
        {
            HideInventoryUI();
            GameEventsManager.instance.playerEvents.EnablePlayerMovement();
        }
        else
        {
            ShowInventoryUI();
            GameEventsManager.instance.playerEvents.DisablePlayerMovement();
        }
    }

    private void ShowInventoryUI()
    {
        _contentParent.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void HideInventoryUI()
    {
        _contentParent.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
