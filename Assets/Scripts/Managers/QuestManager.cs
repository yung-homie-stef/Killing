using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;
    private Dictionary<string, Quest> _questMap;
    [SerializeField] private QuestLogUI _questLogUI;
    [SerializeField] private QuestPopup _questPopup;

    private void Awake()
    {
        if (instance != null)
            Debug.LogError("Found more than one Quest Manager in the scene.");
        instance = this;

        _questMap = CreateQuestMap();
    }

    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.onQuestStart += StartQuest;
        GameEventsManager.instance.questEvents.onQuestAdvance += AdvanceQuest;
        GameEventsManager.instance.questEvents.onQuestFinish += FinishQuest;
        GameEventsManager.instance.questEvents.onQuestStepStateChange += QuestStepStateChange;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.onQuestStart -= StartQuest;
        GameEventsManager.instance.questEvents.onQuestAdvance -= AdvanceQuest;
        GameEventsManager.instance.questEvents.onQuestFinish -= FinishQuest;
        GameEventsManager.instance.questEvents.onQuestStepStateChange -= QuestStepStateChange;
    }

    private void Start()
    {
        foreach (Quest quest in _questMap.Values)
            GameEventsManager.instance.questEvents.QuestStateChange(quest);
    }

    private bool CheckRequirementsMet(Quest quest)
    {
        bool meetsRequirements = true;

        // check for quest pre requisites
        if (quest.info.questPrerequisites != null)
        {
            foreach (SO_QuestInfo prerequisiteQuestInfo in quest.info.questPrerequisites)
            {
                if (GetQuestByID(prerequisiteQuestInfo.id).state != QuestState.FINISHED)
                {
                    meetsRequirements = false;
                }
            }
        }

        return meetsRequirements;
    }

    private void Update()
    {
        foreach (Quest quest in _questMap.Values)
        {
            if (quest.state == QuestState.REQUIREMENTS_NOT_MET && CheckRequirementsMet(quest))
                ChangeQuestState(quest.info.id, QuestState.CAN_START);
        }
    }

    private void ChangeQuestState(string id, QuestState state)
    {
        Quest quest = GetQuestByID(id);
        quest.state = state;
        GameEventsManager.instance.questEvents.QuestStateChange(quest);
    }

    private void StartQuest(string id)
    {
        Debug.Log("Started Quest: " + id);

        Quest quest = GetQuestByID(id);
        quest.InstantiateCurrentQuestStep(this.transform);
        ChangeQuestState(quest.info.id, QuestState.IN_PROGRESS);
        _questLogUI._questLogScrollingList.CreateButtonIfItDoesntExist(quest);
        _questPopup.Popup(quest.info.displayName, QuestState.IN_PROGRESS);
    }

    private void AdvanceQuest(string id)
    {
        Quest quest = GetQuestByID(id);
        quest.MoveToNextQuestStep();

        if (quest.CurrentStepExists())
            quest.InstantiateCurrentQuestStep(this.transform);
        else
            ChangeQuestState(quest.info.id, QuestState.CAN_FINISH);

        Debug.Log("Advanced Quest: " + id);
    }

    private void FinishQuest(string id)
    {
        Quest quest = GetQuestByID(id);
        ChangeQuestState(quest.info.id, QuestState.FINISHED);
        _questPopup.Popup(quest.info.displayName, QuestState.FINISHED);

        Debug.Log("Finished Quest: " + id);
    }

    private void QuestStepStateChange(string id, int stepIndex, QuestStepState questStepState)
    {
        Quest quest = GetQuestByID(id);
        quest.StoreQuestStepState(questStepState, stepIndex);
        ChangeQuestState(id, quest.state);
    }

    private Dictionary<string, Quest> CreateQuestMap()
    {
        SO_QuestInfo[] allQuests = Resources.LoadAll<SO_QuestInfo>("Quests");
        Dictionary<string, Quest> _idToQuestMap = new Dictionary<string, Quest>();

        foreach (SO_QuestInfo questInfo in allQuests)
        {
            if (_idToQuestMap.ContainsKey(questInfo.id))
            {
                Debug.LogWarning("Duplicate ID found when creating the quest map: " + questInfo.id);
            }
            _idToQuestMap.Add(questInfo.id, new Quest(questInfo));
        }
        return _idToQuestMap;
    }

    public Quest GetQuestByID(string id)
    {
        Quest _quest = _questMap[id];
        if (_quest == null)
            Debug.LogError("ID not found in the quest map: " + id);

        return _quest;
    }
}
