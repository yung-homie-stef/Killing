using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestEvents
{
    public event Action<string> onQuestStart;
    public event Action<string> onQuestAdvance;
    public event Action<string> onQuestFinish;
    public event Action<Quest> onQuestStateChange;
    public event Action<string, int, QuestStepState> onQuestStepStateChange;

    public void QuestStart(string id)
    {
        if (onQuestStart != null)
            onQuestStart(id);
    }

    public void QuestAdvance(string id)
    {
        if (onQuestAdvance != null)
            onQuestAdvance(id);
    }

    public void QuestFinish(string id)
    {
        if (onQuestFinish != null)
            onQuestFinish(id);
    }

    public void QuestStateChange(Quest quest)
    {
        if (onQuestStateChange != null)
            onQuestStateChange(quest);
    }

    public void QuestStepStateChange(string id, int stepIndex, QuestStepState stepState)
    {
        if (onQuestStepStateChange != null)
            onQuestStepStateChange(id, stepIndex, stepState);
    }
}