using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public VerticalLayoutGroup inventoryLayoutGroup;
    public VerticalLayoutGroup keyItemInventoryLayoutGroup;
    public GameObject inventoryItemButtonPrefab;

    public void AddItemToInventoryUI(ItemObject itemObj)
    {
        VerticalLayoutGroup vlg;

        if (itemObj.isKeyItem)
            vlg = keyItemInventoryLayoutGroup;
        else
            vlg = inventoryLayoutGroup;


        InventoryItemButton _inventoryItemButton = Instantiate(inventoryItemButtonPrefab, vlg.transform).GetComponent<InventoryItemButton>();
        _inventoryItemButton.Initialize(itemObj);
        _inventoryItemButton.transform.SetAsFirstSibling();
    }
}
