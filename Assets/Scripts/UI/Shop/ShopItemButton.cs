using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using PrimeTween;

public class ShopItemButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TextMeshProUGUI _shopItemName;
    [SerializeField] private TextMeshProUGUI _shopItemPrice;
    [SerializeField] private Image _shopItemTypeIcon;

    private RectTransform _rectTransform;
    private float _height;
    private ItemObject _item;
    private int _price;

    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _height = _rectTransform.sizeDelta.y;
    }

    public void InitializeShopItemButton(ShopItemObject itemObj)
    {
        _item = itemObj._itemToReference;
        _price = itemObj._itemPrice;

        _shopItemName.text = _item.itemName;
        _shopItemPrice.text = "$ " + _price.ToString();
        //_shopItemTypeIcon.sprite = _item.icon;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UIManager.instance._shopMenu.UpdateShopItemDescription(_item);
        Tween.UISizeDelta(target: _rectTransform, endValue: new(475.0f, _height), duration: 0.15f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Tween.UISizeDelta(target: _rectTransform, startValue: _rectTransform.sizeDelta, endValue: new(400.0f, _height), duration: 0.15f);
    }


    public void Purchase()
    {
        //if (MoneyManager.instance.GetCurrentPlayerMoney() < _price)
        MoneyManager.instance.UpdatePlayerMoney(-_price);
        Debug.Log(MoneyManager.instance.GetCurrentPlayerMoney());
    }

}
