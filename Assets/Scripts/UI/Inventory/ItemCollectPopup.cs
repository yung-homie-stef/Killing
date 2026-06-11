using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollectPopup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _itemName;
    [SerializeField] private Image _itemIcon;
    [SerializeField] private TextMeshProUGUI _acquiredRemovedLabel;

    public void InitializePopup(ItemObject item, bool acquired)
    {
        _itemName.text = item.itemName;
        _itemIcon.sprite = item.itemIcon;

        if (acquired)
            _acquiredRemovedLabel.text = "ITEM ACQUIRED";
        else
            _acquiredRemovedLabel.text = "ITEM REMOVED";
    }
}
