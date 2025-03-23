using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputEvents : MonoBehaviour
{
    public event Action onQuestLogTogglePressed;

    public void QuestLogTogglePressed()
    {
        if (onQuestLogTogglePressed != null)
            onQuestLogTogglePressed();
    }
}
