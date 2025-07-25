using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Interactable
{
    public ItemObject item;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact()
    {
        base.Interact();

        if (!item.isKeyItem)
            UIManager.instance.inventoryUI.AddItemToInventoryUI(item, InventoryManager.instance._itemInventory.AddItem(item));
        else
            UIManager.instance.inventoryUI.AddItemToInventoryUI(item, InventoryManager.instance._keyItemInventory.AddItem(item));

        Destroy(gameObject);
    }
}
