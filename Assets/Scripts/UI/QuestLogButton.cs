using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class QuestLogButton : MonoBehaviour, ISelectHandler, IPointerEnterHandler
{
    public Button button { get; private set; }

    private UnityAction _onSelectAction;
    private TextMeshProUGUI _buttonQuestText;
    private QuestLogUI _questLogUI;

    [HideInInspector]
    public Quest _questForButton;

    void Awake()
    {
        _questLogUI = GetComponentInParent<QuestLogUI>();
    }

    public void OnSelect(BaseEventData eventData)
    {
        _onSelectAction();
    }

    public void Initialize(string displayName, UnityAction selectAction, Quest quest)
    {
        this.button = this.GetComponent<Button>();
        this._buttonQuestText = this.GetComponentInChildren<TextMeshProUGUI>();

        this._buttonQuestText.text = displayName;
        this._onSelectAction = selectAction;
        this._questForButton = quest;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _questLogUI.SetQuestLogInfo(_questForButton);
    }

    //public void OnPointerExit(PointerEventData eventData)
    //{
    //    _questLogUI.ClearQuestLogInfo();
    //}

    public void SetState(QuestState state)
    {
        // temp code until proper visuals for completed quests has been developed
        switch (state)
        {
            case QuestState.IN_PROGRESS:
                _buttonQuestText.color = Color.yellow;
                break;
            case QuestState.CAN_FINISH:
                _buttonQuestText.color = Color.green;
                break;
            case QuestState.FINISHED:
                _buttonQuestText.color = Color.gray;
                break;
        }
    }

}
