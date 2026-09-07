using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomUIQuestTracker : StandardUIQuestTracker
{
    [SerializeField] private RectTransform _questBanner;

   protected override IEnumerator RefreshAtEndOfFrame()
    {
        int numTracked = 0;
        QuestState flags = (showActiveQuests ? (QuestState.Active | QuestState.ReturnToNPC) : 0) |
                (showCompletedQuests ? (QuestState.Success | QuestState.Failure) : 0);
        foreach (string quest in QuestLog.GetAllQuests(flags))
        {
            if (QuestLog.IsQuestTrackingEnabled(quest))
            {
                numTracked++;
            }
        }

        if (numTracked == 0)
            _questBanner.gameObject.SetActive(false);
        else
            _questBanner.gameObject.SetActive(true);

       return base.RefreshAtEndOfFrame();
    }
}
