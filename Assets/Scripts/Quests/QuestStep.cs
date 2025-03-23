using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestStep : MonoBehaviour
{
    private bool _isFinished = false;

    private string _questID;
    private int _stepIndex;

    public void InitializeQuestStep(string questID, int stepIndex)
    {
        this._questID = questID;
        this._stepIndex = stepIndex;
    }

    protected void FinishQuestStep()
    {
        if (!_isFinished)
        {
            _isFinished = true;
            GameEventsManager.instance.questEvents.QuestAdvance(_questID);
            Destroy(this.gameObject);
        }
    }

    protected void ChangeState(string newState, string newStatus)
    {
        GameEventsManager.instance.questEvents.QuestStepStateChange(_questID, _stepIndex, new QuestStepState(newState, newStatus));
    }
}
