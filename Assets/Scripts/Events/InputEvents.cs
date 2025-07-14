using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputEvents
{
    public event Action onQuestLogTogglePressed;
    public event Action onInventoryToggledPressed;

    public void QuestLogTogglePressed()
    {
        if (onQuestLogTogglePressed != null)
            onQuestLogTogglePressed();
    }

    public void InventoryTogglePressed()
    {
        if (onInventoryToggledPressed != null)
            onInventoryToggledPressed();
    }
}
