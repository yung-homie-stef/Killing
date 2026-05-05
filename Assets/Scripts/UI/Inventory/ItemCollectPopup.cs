using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollectPopup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _itemName;
    [SerializeField] private Image _itemIcon;

    public void InitializePopup(ItemObject item)
    {
        _itemName.text = item.itemName;
        _itemIcon.sprite = item.itemIcon;
    }
}
