using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ShopData", menuName = "ScriptableObjects/ShopData", order = 5)]
public class ShopData : ScriptableObject
{
    public string shopName;
    public ShopItemObject[] shopStock;
    //public Sprite shopLogo;
}
