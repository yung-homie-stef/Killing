using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_QuestInfo", menuName = "ScriptableObjects/QuestInfo", order = 2)]
public class SO_QuestInfo : ScriptableObject
{
    [field: SerializeField] public string id { get; private set; }

    [Header("General")]
    public string displayName;
    public string displayDescription = new string("This is the base description for the quest");
    public string displayFlavourText = new string("This is the base flavour text for the quest");
    public SO_QuestInfo[] questPrerequisites;

    [Header("QuestSteps")]
    public GameObject[] questStepPrefabs;

    [Header("Dialogue")]
    public Conversation assignmentConversation;
    public Conversation completionConversation;
    public Conversation inProgressConversation;
}
