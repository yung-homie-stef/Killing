using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Conversation", order = 1)]
public class Conversation : ScriptableObject 
{
    public string[] lines;
    public bool canIncreaseIndex = false;
}
