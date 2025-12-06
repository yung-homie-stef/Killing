using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopItemButton : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private TextMeshProUGUI _shopItemName;
    [SerializeField] private TextMeshProUGUI _shopItemPrice;
    [SerializeField] private Image _shopItemTypeIcon;

    private ItemObject _item;

    public void InitializeShopItemButton(ShopItemObject itemObj)
    {
        _item = itemObj._itemToReference;

        _shopItemName.text = _item.itemName;
        _shopItemPrice.text = itemObj._itemPrice.ToString();
        //_shopItemTypeIcon.sprite = _item.icon;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UIManager.instance._shopMenu.UpdateShopItemDescription(_item.itemDescription);
    }
}
