using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInteractable : Interactable
{
    public ShopData shopData;

    public override void Interact()
    {
        base.Interact();
        UIManager.instance._shopMenu.InitializeShop(shopData.shopStock, shopData.name);

    }
}
