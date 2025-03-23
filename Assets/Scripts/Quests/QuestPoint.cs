using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// TODO: TEST SCRIPT UNTIL I WORK IN QUEST POINT LOGIC INTO CHARACTER INTERACTABLES

//[RequireComponent(typeof(Interactable))] commented this out for the purpose of testing with triggers
public class QuestPoint : MonoBehaviour
{
    [Header("Quest")]
    [SerializeField] private SO_QuestInfo _questInfoForPoint;
    [Header("Config")]
    [SerializeField] private bool _isStartPoint = false;
    [SerializeField] private bool _isEndPoint = false;
    private string _questID;
    private QuestState _currentQuestState;
    private QuestIcon _questIcon;

    private bool _playerIsNear = false;

    private void Awake()
    {
        _questID = _questInfoForPoint.id;
        _questIcon = GetComponentInChildren<QuestIcon>();
    }

    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.onQuestStateChange += QuestStateChange;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.onQuestStateChange -= QuestStateChange;
    }

    private void Update()
    {
        //TODO: Change this to a subscribed event involving input

        if (_playerIsNear)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (_currentQuestState.Equals(QuestState.CAN_START) && _isStartPoint)
                    GameEventsManager.instance.questEvents.QuestStart(_questID);

                else if (_currentQuestState.Equals(QuestState.CAN_FINISH) && _isEndPoint)
                    GameEventsManager.instance.questEvents.QuestFinish(_questID);
            }


        }
    }

    private void QuestStateChange(Quest quest)
    {
        if (quest.info.id.Equals(_questID))
        {
            _currentQuestState = quest.state;
            _questIcon.SetState(_currentQuestState, _isStartPoint, _isEndPoint);
            Debug.Log("Quest with id: " + _questID + " updated to state: " + _currentQuestState);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            _playerIsNear = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            _playerIsNear = false;
    }
}
