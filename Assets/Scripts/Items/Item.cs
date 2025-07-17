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
        UIManager.instance.inventoryUI.AddItemToInventoryUI(item, InventoryManager.instance._itemInventory.AddItem(item));
        Destroy(gameObject);
    }
}
