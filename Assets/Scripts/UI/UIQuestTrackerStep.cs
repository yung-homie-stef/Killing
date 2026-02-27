using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIQuestTrackerStep : MonoBehaviour
{
    [SerializeField] private string _questStepString = "";
    [SerializeField] private TextMeshProUGUI _questStepDetails = null;

    void InitializeQuestStep()
    {
        _questStepDetails.text = _questStepString;
    }
    
}
