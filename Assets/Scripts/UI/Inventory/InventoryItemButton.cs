using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.Events;

public class InventoryItemButton : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _itemName;
    private ItemObject _itemObject;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void Initialize(ItemObject itemObj)
    {
        _itemObject = itemObj;
        _itemName.text = itemObj.itemName;
    }
}
