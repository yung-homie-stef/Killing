using System.Collections;
using System.Collections.Generic;
using UnityEditor.TerrainTools;
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
    public Mesh itemMesh;
    public Material itemMaterial;
    public ItemType itemType;
    public bool isKeyItem = false;
    public string itemName;
    [TextArea(5,20)]
    public string itemDescription;
}
