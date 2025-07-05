using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Food, 
    Beverage, 
    Drug, 
    Quest,
    Default
}

public abstract class ItemObject : ScriptableObject
{
    public GameObject itemPrefab;
    public ItemType itemType;
    public string itemName;
    [TextArea(15,20)]
    public string itemDescription;
}
