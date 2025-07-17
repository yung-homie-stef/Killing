using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance { get; private set; }
    public InventoryObject _itemInventory;
    public InventoryObject _keyItemInventory;

    private void Awake()
    {
        if (instance != null)
            Debug.LogError("Found more than one Inventory Manager in the scene.");
        instance = this;
    }
}
