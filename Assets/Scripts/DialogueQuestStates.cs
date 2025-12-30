using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class DialogueQuestStates : MonoBehaviour
{
    void OnQuestStateChange(string questName)
    {
        if (QuestLog.IsQuestActive(questName))
        {
            DialogueManager.ShowAlert(questName);
        }
    }
}
