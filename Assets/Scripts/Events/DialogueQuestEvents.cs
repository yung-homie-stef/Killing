using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueQuestEvents : MonoBehaviour
{
    void OnQuestStateChange(string questTitle)
    {
        DialogueManager.instance.ShowAlert(questTitle);
    }

    void OnQuestSEntryStateChange(string questTitle)
    {

    }
}
