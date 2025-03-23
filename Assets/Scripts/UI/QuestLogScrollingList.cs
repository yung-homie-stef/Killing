using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestLogScrollingList : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject _contentParent;

    [Header("Rect Transforms")]
    [SerializeField] private RectTransform _scrollRectTransform;
    [SerializeField] private RectTransform _contentRectTransform;

    [Header("Quest Log Button")]
    [SerializeField] private GameObject _questLogButtonPrefab;

    private Dictionary<string, QuestLogButton> _IDButtonToMap = new Dictionary<string, QuestLogButton>();

    #region temp start method
    //private void Start()
    //{
    //    for (int i = 0; i < 18; i++)
    //    {
    //        SO_QuestInfo questInfoTest = ScriptableObject.CreateInstance<SO_QuestInfo>();
    //        questInfoTest.id = "test_" + i;
    //        questInfoTest.displayName = "Test " + i;
    //        questInfoTest.questStepPrefabs = new GameObject[0];
    //        Quest quest = new Quest(questInfoTest);

    //        QuestLogButton questLogButton = CreateButtonIfItDoesntExist(quest, () => {
    //            Debug.Log("SELECTED: " + questInfoTest.displayName);
    //        });

    //        if (i == 0)
    //        {
    //            questLogButton.button.Select();
    //        }
    //    }
    //}
    #endregion

    public QuestLogButton CreateButtonIfItDoesntExist(Quest quest)
    {
        QuestLogButton questLogButton = null;

        if (!_IDButtonToMap.ContainsKey(quest.info.id))
            questLogButton = InstantiateQuestLogButton(quest);
        else
            questLogButton = _IDButtonToMap[quest.info.id];

        return questLogButton;
    }


    private QuestLogButton InstantiateQuestLogButton(Quest quest)
    {
        QuestLogButton questLogButton = Instantiate(_questLogButtonPrefab, _contentParent.transform).GetComponent<QuestLogButton>();

        questLogButton.gameObject.name = quest.info.id + "_button";
        RectTransform buttonRectTransform = questLogButton.GetComponent<RectTransform>();
        questLogButton.Initialize(quest.info.displayName, () =>
        {
            UpdateScrolling(buttonRectTransform);
        }, quest);

        _IDButtonToMap[quest.info.id] = questLogButton;
        return questLogButton;
    }

    private void UpdateScrolling(RectTransform buttonRectTransform)
    {
        //calculating the dimensions for the selected button
        float buttonYMin = Mathf.Abs(buttonRectTransform.anchoredPosition.y);
        float buttonYMax = buttonYMin + buttonRectTransform.rect.height;

        //calculating the dimensions for the content area
        float contentYMin = _contentRectTransform.anchoredPosition.y;
        float contentYMax = contentYMin + _scrollRectTransform.rect.height;

        // scrolling down
        if (buttonYMax > contentYMax)
            _contentRectTransform.anchoredPosition = new Vector2(_contentRectTransform.anchoredPosition.x, buttonYMax - _scrollRectTransform.rect.height);

        // scrolling up
        if (buttonYMin < contentYMin)
            _contentRectTransform.anchoredPosition = new Vector2(_contentRectTransform.anchoredPosition.x, buttonYMin);
    }

    public QuestLogButton GetButton(Quest quest)
    {
        return _IDButtonToMap[quest.info.id];
    }
    
}
