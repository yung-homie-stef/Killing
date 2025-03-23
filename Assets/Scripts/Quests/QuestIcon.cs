using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestIcon : MonoBehaviour
{
    // TODO: Change icon objects to be one single object that swaps icons
    [Header("Icons")]
    [SerializeField] private GameObject _requirementsNotMetToStartIcon;
    [SerializeField] private GameObject _canStartIcon;
    [SerializeField] private GameObject _requirementsNotMetToFinishIcon;
    [SerializeField] private GameObject _canFinishIcon;

    public void SetState(QuestState newState, bool startPoint, bool finishPoint)
    {
        switch (newState)
        {
            case QuestState.REQUIREMENTS_NOT_MET:
                if (startPoint) { _requirementsNotMetToStartIcon.SetActive(true); }
                break;
            case QuestState.CAN_START:
                if (startPoint) { _canStartIcon.SetActive(true); }
                break;
            case QuestState.IN_PROGRESS:
                if (startPoint) { _requirementsNotMetToFinishIcon.SetActive(true); }
                break;
            case QuestState.CAN_FINISH:
                if (startPoint) { _canFinishIcon.SetActive(true); }
                break;
            case QuestState.FINISHED:
                break;
                default:
                Debug.LogWarning("Quest State not recognized by switch statment for quest icons: " + newState); 
                break;
        }
    }
}
