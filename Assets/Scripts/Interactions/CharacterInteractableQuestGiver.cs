using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteractableQuestGiver : CharacterInteractable
{
    [Header("Quest Parameters")]
    [SerializeField]
    private SO_QuestInfo[] _quests;
    [SerializeField]
    private static List<string> _questIDs = new List<string>();
    private string _currentAssignedQuestID = null;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        for (int i = 0; i < _quests.Length; i++)
        {
            _questIDs.Add(_quests[i].id);
        }

    }

   public override void Interact()
   {
       base.Interact();

       if (_canSpeak)
       {
            SetUIManagerDialogue(_conversations[_conversationIndex]); // fill it up with default convo values by default

            for (int i =0; i < _questIDs.Count; i++) 
            {
                QuestState qs = QuestManager.instance.GetQuestByID(_questIDs[i]).state;

                if (qs.Equals(QuestState.CAN_START)) // change dialogue if quest can be started
                {
                    _currentAssignedQuestID = _questIDs[i];
                    SetUIManagerDialogue(_quests[i].assignmentConversation);
                    break;
                }
                else if (qs.Equals(QuestState.CAN_FINISH)) // change dialogue if quest can be finished
                {
                    SetUIManagerDialogue(_quests[i].completionConversation);
                    break;
                }
                else if (qs.Equals(QuestState.IN_PROGRESS)) // change dialogue if quest is in progress
                {
                    SetUIManagerDialogue(_quests[i].inProgressConversation);
                }
            }

            BeginDialogue();
       }
   }

    private void BeginDialogue()
    {
        _cinemachineVirtualCamera.Priority = 1;
        UIManager.instance.dialogue.StartDialogue(this);
        UIManager.instance.player.SetPlayerControls(false);
    }

    public override void Disengage()
    {
        base.Disengage();

        // start a quest once the NPC is done talking
        if (QuestManager.instance.GetQuestByID(_currentAssignedQuestID).state == QuestState.CAN_START)
            GameEventsManager.instance.questEvents.QuestStart(_currentAssignedQuestID);

        else if (QuestManager.instance.GetQuestByID(_currentAssignedQuestID).state == QuestState.CAN_FINISH)
            GameEventsManager.instance.questEvents.QuestFinish(_currentAssignedQuestID);
    }

    private void SetUIManagerDialogue(Conversation convo)
    {
        UIManager.instance.dialogue.SetConversation(convo); 
    }

}
