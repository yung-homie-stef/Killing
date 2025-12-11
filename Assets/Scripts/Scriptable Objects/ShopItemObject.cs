using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(fileName = "New Shop Item", menuName = "ScriptableObjects/ShopItem", order = 4)]
public class ShopItemObject : ScriptableObject
{
    public ItemObject _itemToReference;
    public int _itemPrice;
    public bool _canBuyMultiple = false;
}
