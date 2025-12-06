using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private RectTransform _itemButtonContentParent;
    [SerializeField] private GameObject _itemButtonPrefab;
    [Header("Text")]
    [SerializeField] private TextMeshProUGUI _shopName;
    [SerializeField] private TextMeshProUGUI _playerFunds;
    [SerializeField] private TextMeshProUGUI _shopItemDescription;
    [Header("Stock")]
    [SerializeField] private List<ShopItemObject> _itemsAvailableInShop;

    // test variables
    [SerializeField] public ShopItemObject[] swag;

    private void Awake()
    {
        InitializeShop(swag, "Fat Boy Dumplings");
        _playerFunds.text = PlayerPrefs.GetInt("Player$$$").ToString();
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
        }

        // set shop description to first item by default so shop doesn't open with empty text box
        _shopItemDescription.text = "DESCRIPTION: " + _itemsAvailableInShop[0]._itemToReference.itemDescription;
    }

    public void UpdateShopItemDescription(string desc)
    {
        _shopItemDescription.text = "DESCRIPTION: " + desc;
    }

    private void ClearShop()
    {
        if (_itemsAvailableInShop.Count != 0)
            _itemsAvailableInShop.Clear();

        _shopItemDescription.text = string.Empty;
    }
}
