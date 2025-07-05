using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Default Item", menuName = "ScriptableObjects/Item/Default", order = 3)]
public class DefaultItemObject : ItemObject
{
   public void Awake()
   {
       itemType = ItemType.Default;
   }
}
