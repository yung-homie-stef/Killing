using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "ScriptableObjects/Inventory", order = 3)]
public class InventoryObject : ScriptableObject
{
    public List<InventorySlot> container = new List<InventorySlot>();
    public InventorySlot AddItem(ItemObject _item)
    {
       InventorySlot IS = new InventorySlot(_item);
       container.Add(IS);
       return IS;
    }

    public void RemoveItem(InventorySlot slot)
    {
        container.Remove(slot);
    }
}

[System.Serializable]
public class InventorySlot
{
    public ItemObject item;
    public InventorySlot(ItemObject _item)
    {
        item = _item;
    }

}

