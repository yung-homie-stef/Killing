using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_QuestInfo", menuName = "ScriptableObjects/QuestInfo", order = 4)]
public class SO_StoreStock : ScriptableObject
{
    public SO_StoreItem[] stockedItems;
}
