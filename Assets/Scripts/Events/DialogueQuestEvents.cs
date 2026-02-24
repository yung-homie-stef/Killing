using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueQuestEvents : MonoBehaviour
{
    void OnQuestStateChange(string questTitle)
    {
        if (QuestLog.IsQuestActive(questTitle))
            DialogueManager.instance.ShowAlert(questTitle);
        else if (QuestLog.IsQuestSuccessful(questTitle))
            DialogueManager.instance.ShowAlert(questTitle + " is done.");
    }
    void OnQuestEntryStateChange(QuestEntryArgs args)
    {
        Debug.Log("swag");
    }
}
