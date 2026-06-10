using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;
using UnityEngine.UI;

public class SubtitleFastForwardDelay : MonoBehaviour
{
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnConversationLine(Subtitle subtitle)
    {
        _button.interactable = false;
        Invoke(nameof(AllowButtonToBeInteractable), 0.75f);
    }

    private void AllowButtonToBeInteractable()
    {
        _button.interactable = true;
    }
}
