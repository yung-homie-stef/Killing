using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest 
{
    public SO_QuestInfo info;

    public QuestState state;
    [SerializeField]
    private int currentQuestStepIndex;
    [SerializeField]
    private QuestStepState[] _questStepStates;

    public Quest(SO_QuestInfo questInfo)
    {
        this.info = questInfo;
        this.state = QuestState.REQUIREMENTS_NOT_MET;
        this.currentQuestStepIndex = 0;
        this._questStepStates = new QuestStepState[questInfo.questStepPrefabs.Length];

        for (int i =0; i < _questStepStates.Length; i++)
        {
            _questStepStates[i] = new QuestStepState();
        }
    }

    public void MoveToNextQuestStep()
    {
        currentQuestStepIndex++;
    }

    public bool CurrentStepExists()
    {
        return (currentQuestStepIndex < info.questStepPrefabs.Length);
    }

    public void InstantiateCurrentQuestStep(Transform parentTransform)
    {
        GameObject _questStepPrefab = GetCurrentQuestStepPrefab();
        if (_questStepPrefab != null)
        {
            QuestStep questStep = Object.Instantiate<GameObject>(_questStepPrefab, parentTransform).GetComponent<QuestStep>();
            questStep.InitializeQuestStep(info.id, currentQuestStepIndex);
        }
    }

    private GameObject GetCurrentQuestStepPrefab()
    {
        GameObject _questStepPrefab = null;
        if (CurrentStepExists())
            _questStepPrefab = info.questStepPrefabs[currentQuestStepIndex];
        //else
        //    Debug.Log("Tried to get quest step prefab but questStepIndex was out of bounds. There is no current step. QuestID = " + questInfo.id + " StepIndex = " + currentQuestStepIndex);

        return _questStepPrefab;
    }

    public void StoreQuestStepState(QuestStepState questStepState, int stepIndex)
    {
        if (stepIndex < _questStepStates.Length)
        {
            _questStepStates[stepIndex].state = questStepState.state;
            _questStepStates[stepIndex].status = questStepState.status;
        }
        else
            Debug.LogWarning("Tried to access quest step data but index was out of range:" + "QuestID = " + info.id + ", StepIndex = " + stepIndex);
    }

    public string GetFullStatusText()
    {
        string fullStatus = "";
        Debug.Log("Current QSI is: " + currentQuestStepIndex);

        for (int i=0; i < currentQuestStepIndex; i++)
        {
            Debug.Log(_questStepStates[i].status);
            // <s> being a strike through and \n being a new line
            // cycles through all completed quest steps
            fullStatus += "<s>" + _questStepStates[i].status + "</s>\n";
        }

        // add the incomplete current step to the full status
        if (CurrentStepExists())
            fullStatus += _questStepStates[currentQuestStepIndex].status;

        if (state == QuestState.CAN_FINISH)
            fullStatus += "\n" + "\n" + "The quest is ready to be turned in";

        return fullStatus;
    }
}
