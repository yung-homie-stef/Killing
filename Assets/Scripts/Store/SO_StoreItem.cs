using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Store Item", order = 3)]
public class SO_StoreItem : ScriptableObject
{
    public string itemName = string.Empty;
    public string itemDescription = string.Empty;
    public Mesh itemMesh = null;
    public int itemPrice = 0;

    public enum StoreItemType
    {
        Beverage,
        Food,
        Drug
    }

}
