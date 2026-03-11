using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using UnityEditor.TerrainTools;
using UnityEngine;

[ExecuteInEditMode]

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
    public ItemType itemType;
    public bool isKeyItem = false;
    public Sprite itemThumbnail;
    public Sprite itemIcon;
    public string itemName;
    [TextArea(5,20)]
    public string itemDescription;

}

