using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestPopup : MonoBehaviour
{
    private CustomTweening _tweening;
    [SerializeField] private TextMeshProUGUI _taskAssignedText;
    [SerializeField] private TextMeshProUGUI _taskCompletedText;
    [SerializeField] private TextMeshProUGUI _assignmentNameText;

    // Start is called before the first frame update
    void Start()
    {
        _tweening = GetComponentInParent<CustomTweening>();
    }

    public void Popup(string questDisplayName, QuestState qs)
    {
        _taskCompletedText.gameObject.SetActive(false);
        _taskAssignedText.gameObject.SetActive(false);

        if (qs == QuestState.IN_PROGRESS)
            _taskAssignedText.gameObject.SetActive(true);
        else if (qs == QuestState.FINISHED)
            _taskCompletedText.gameObject.SetActive(true);

        _tweening.OpenWindow();
        _assignmentNameText.text = questDisplayName;
        StartCoroutine(CloseWindowDelay(2.5f));
    }

    private IEnumerator CloseWindowDelay(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        _tweening.CloseWindow();
    }


}
