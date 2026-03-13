using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private RectTransform _itemButtonContentParent;
    [SerializeField] private GameObject _itemButtonPrefab;
    [SerializeField] private GameObject _content;
    [SerializeField] private Image _itemImage;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI _shopName;
    [SerializeField] private TextMeshProUGUI _playerFunds;
    [SerializeField] private TextMeshProUGUI _shopItemDescription;
    [SerializeField] private TextMeshProUGUI _shopItemName;

    [Header("Stock")]
    [SerializeField] private List<ShopItemObject> _itemsAvailableInShop;
    [SerializeField] private List<ShopItemButton> _buttonsInShop;
    private ShopItemButton _itemNeededToBuy = null;

    private void Awake()
    {
        _playerFunds.text = PlayerPrefs.GetInt("Player$$$").ToString();
    }

    private void OnEnable()
    {
        GameEventsManager.instance.moneyEvents.onMoneyAmountChanged += UpdatePlayerFundsAmount;
        Lua.RegisterFunction("InitializeShopFromDialogue", this, SymbolExtensions.GetMethodInfo(() => InitializeShopFromDialogue(string.Empty, string.Empty)));
    }

    private void OnDisable()
    {
        GameEventsManager.instance.moneyEvents.onMoneyAmountChanged -= UpdatePlayerFundsAmount;
    }

    public void ShopToggle(bool flag)
    {
        Cursor.visible = flag;
        _content.SetActive(flag);

        if (flag)
        {
            GameEventsManager.instance.playerEvents.DisablePlayerMovement();
            UIManager.instance._hudMenu.HUDTween(false);
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            GameEventsManager.instance.playerEvents.EnablePlayerMovement();
            UIManager.instance._hudMenu.HUDTween(true);
            Cursor.lockState = CursorLockMode.Locked;
            UIManager.instance.focusUI.SetCanFocus(true);
        }
    }

    public void InitializeShop(ShopData data)
    {
        ClearShop();

        _shopName.text = data.shopName.ToUpper() + ".";

        foreach (ShopItemObject item in data.shopStock)
            _itemsAvailableInShop.Add(item);

        _itemsAvailableInShop.Sort((leftHandSide, rightHandSide) => leftHandSide.name.CompareTo(rightHandSide.name));

        for (int i =0; i <  _itemsAvailableInShop.Count; i++)
        {
            var shopItem = Instantiate(_itemButtonPrefab, _itemButtonContentParent.transform);
            ShopItemButton shopItemButton = shopItem.GetComponent<ShopItemButton>();
            shopItemButton.InitializeShopItemButton(_itemsAvailableInShop[i]);
            _buttonsInShop.Add(shopItemButton);
        }

        // set shop description to first item by default so shop doesn't open with empty text box
        UpdateShopItemDescription(_itemsAvailableInShop[0]._itemToReference);

        ShopToggle(true);
    }

    public void UpdateShopItemDescription(ItemObject itemObject)
    {
        _shopItemDescription.text =  itemObject.itemDescription;
        _itemImage.sprite = itemObject.itemThumbnail;
        _shopItemName.text = " " + itemObject.itemName;
    }

    public void InitializeShopFromDialogue(string shopDataName, string itemToBuy)
    {
        InitializeShop(Resources.Load<ShopData>("ShopData/" + shopDataName));

        for (int i =0; i < _itemsAvailableInShop.Count; i++)
        {
            if (_buttonsInShop[i]._item.itemName == itemToBuy)
            {
                _buttonsInShop[i].gameObject.SetActive(true);
                break;
            }
            else
                Debug.Log("Specified item could not be found in the shop.");
        }
       
    }

    public void PurchaseFromShop(ShopItemButton itemButton, int price)
    {
        // TODO: add this line back when i can test money again, right now im below zero
        //if (MoneyManager.instance.GetCurrentPlayerMoney() < _price)

        if (itemButton == _itemNeededToBuy)
        {
            MoneyManager.instance.UpdatePlayerMoney(-price);
            if (itemButton._canRemove)
            {
                _buttonsInShop.Remove(itemButton);
                PixelCrushers.DialogueSystem.Sequencer.Message("ItemBought");
            }
            // Debug.Log(MoneyManager.instance.GetCurrentPlayerMoney());
        }
    }



    private void ClearShop()
    {
        if (_buttonsInShop.Count != 0)
        {
            for (int i = 0; i < _buttonsInShop.Count; i++)
                Destroy(_buttonsInShop[i].gameObject);    
        }
        
        _buttonsInShop.Clear();
        _itemsAvailableInShop.Clear();
        _shopItemDescription.text = string.Empty;
    }

    private void UpdatePlayerFundsAmount(int previousAmount, int amount)
    {
        _playerFunds.text = amount.ToString();
    }
}
