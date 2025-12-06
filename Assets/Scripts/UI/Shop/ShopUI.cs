using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class ShopUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private RectTransform _itemButtonContentParent;
    [SerializeField] private GameObject _itemButtonPrefab;
    [SerializeField] private GameObject _content;
    [Header("Text")]
    [SerializeField] private TextMeshProUGUI _shopName;
    [SerializeField] private TextMeshProUGUI _playerFunds;
    [SerializeField] private TextMeshProUGUI _shopItemDescription;
    [Header("Stock")]
    [SerializeField] private List<ShopItemObject> _itemsAvailableInShop;
    [SerializeField] private List<ShopItemButton> _buttonsInShop;

    private void Awake()
    {
        _playerFunds.text = PlayerPrefs.GetInt("Player$$$").ToString();
    }

    private void OnEnable()
    {
        GameEventsManager.instance.moneyEvents.onMoneyAmountChanged += UpdatePlayerFundsAmount;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.moneyEvents.onMoneyAmountChanged -= UpdatePlayerFundsAmount;
    }

    public void ShowOrHideShopUI(bool flag)
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

    public void InitializeShop(ShopItemObject[] stock, string shopName)
    {
        ClearShop();

        _shopName.text = shopName.ToUpper() + ".";

        foreach (ShopItemObject item in stock)
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
        _shopItemDescription.text = "DESCRIPTION: " + _itemsAvailableInShop[0]._itemToReference.itemDescription;

        ShowOrHideShopUI(true);
    }

    public void UpdateShopItemDescription(string desc)
    {
        _shopItemDescription.text = "DESCRIPTION: " + desc;
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

    private void UpdatePlayerFundsAmount(int amount)
    {
        _playerFunds.text = amount.ToString();
    }
}
