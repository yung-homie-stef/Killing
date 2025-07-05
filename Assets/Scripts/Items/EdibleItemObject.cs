using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Food Item", menuName = "ScriptableObjects/Item/Edible", order = 4)]
public class EdibleItemObject : ItemObject
{
   public void Awake()
   {
       itemType = ItemType.Food;
   }
}
