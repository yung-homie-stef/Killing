using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.InteropServices;
using UnityEngine.EventSystems;

public class QuestLogUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject _contentParent;
    [SerializeField] public QuestLogScrollingList _questLogScrollingList;
    [SerializeField] private TextMeshProUGUI _questTitleText;
    [SerializeField] private TextMeshProUGUI _questDescriptionText;
    [SerializeField] private TextMeshProUGUI _questFlavourText;
    [SerializeField] private TextMeshProUGUI _questFullStatusText;

    [SerializeField]
    private Button _currentlySelectedButton;

    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.onQuestStateChange += QuestStateChange;
        GameEventsManager.instance.inputEvents.onQuestLogTogglePressed += QuestLogButtonToggle;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.onQuestStateChange -= QuestStateChange;
        GameEventsManager.instance.inputEvents.onQuestLogTogglePressed -= QuestLogButtonToggle;
    }

    private void QuestStateChange(Quest quest)
    {
        QuestLogButton questLogButton;

        if (quest.state == QuestState.IN_PROGRESS)
        {
            questLogButton = _questLogScrollingList.CreateButtonIfItDoesntExist(quest);
            if (_currentlySelectedButton == null)
            {
                _currentlySelectedButton = questLogButton.button;
                _currentlySelectedButton.Select();
                SetQuestLogInfo(quest);
            }
            questLogButton.SetState(quest.state);
        }

        else if (quest.state == QuestState.CAN_FINISH || quest.state == QuestState.FINISHED)
        { 
            questLogButton = _questLogScrollingList.GetButton(quest);
            questLogButton.SetState(quest.state);
        }
        
    }

    private void QuestLogButtonToggle()
    {
        if (_contentParent.activeInHierarchy)
        {
            HideQuestLogUI();
            GameEventsManager.instance.playerEvents.EnablePlayerMovement();
        }
        else
        {
            ShowQuestLogUI();
            GameEventsManager.instance.playerEvents.DisablePlayerMovement();
        }
    }

    public void SetQuestLogInfo(Quest quest)
    {
        _questTitleText.text = quest.info.displayName;
        _questDescriptionText.text = quest.info.displayDescription;
        _questFlavourText.text = quest.info.displayFlavourText;
        _questFullStatusText.text = quest.GetFullStatusText();

    }

    public void ClearQuestLogInfo()
    {
        _questTitleText.text = "";
        _questDescriptionText.text = "";
        _questFlavourText.text = "";
        _questFullStatusText.text = "";
    }

    private void ShowQuestLogUI()
    {
        GameEventsManager.instance.playerEvents.DisablePlayerMovement();
        _contentParent.SetActive(true);

        // to display text on startup
        _questFullStatusText.text = _currentlySelectedButton.GetComponent<QuestLogButton>()._questForButton.GetFullStatusText();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }

    private void HideQuestLogUI()
    {
        GameEventsManager.instance.playerEvents.EnablePlayerMovement();
        _contentParent.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
