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
    [SerializeField] private TextMeshProUGUI _itemDescriptionText;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private UseDiscardPrompt _useDiscardPrompt;
    [HideInInspector] public Interactable targetInteractable = null;

    [Header("Item Display")]
    [SerializeField] private MeshFilter _filter;
    [SerializeField] private MeshRenderer _renderer;

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

    public void AddItemToInventoryUI(ItemObject itemObj, InventorySlot slot)
    {
       VerticalLayoutGroup vlg;

       if (itemObj.isKeyItem)
           vlg = _keyItemInventoryLayoutGroup;
       else
           vlg = _inventoryLayoutGroup;


        InventoryItemButton _inventoryItemButton = Instantiate(_inventoryItemButtonPrefab, vlg.transform).GetComponent<InventoryItemButton>();
        _inventoryItemButton.Initialize(itemObj, slot);
        _inventoryItemButton.transform.SetAsFirstSibling();
        _inventoryItemButton.name = itemObj.name;
        _inventoryItemButtonList.Add(_inventoryItemButton);
    }

    public void UpdateInventoryItemDisplay(ItemObject itemObj)
    {
        _itemDescriptionText.text = itemObj.itemDescription;
        _filter.sharedMesh = itemObj.itemMesh;
        _renderer.material = itemObj.itemMaterial;

    }

    public void ResetInventoryItemDisplay()
    {
        _itemDescriptionText.text = string.Empty;
    }

    public void PromptItemUseOrDiscard(InventoryItemButton invButton, InventorySlot invSlot, bool isUsing)
    {
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
        _useDiscardPrompt.InitializePrompt(invButton, invSlot, isUsing);
    }

    public void CloseItemUseOrDiscard()
    {
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
    }

    public void RemoveItemFromInventoryUI(InventoryItemButton itemButton)
    {
        _inventoryItemButtonList.Remove(itemButton);
        Destroy(itemButton.gameObject);
    }

    private void InventoryButtonToggle(bool flag)
    {
        if (flag)
        {
            ShowOrHideInventoryUI(flag, CursorLockMode.None);
            GameEventsManager.instance.playerEvents.DisablePlayerMovement();
        }
        else
        {
            ShowOrHideInventoryUI(flag, CursorLockMode.Locked);
            GameEventsManager.instance.playerEvents.EnablePlayerMovement();
        }
    }

    private void ShowOrHideInventoryUI(bool flag, CursorLockMode mode)
    {
        _contentParent.SetActive(flag);
        Cursor.lockState = mode;
        Cursor.visible = flag;
    }
}
