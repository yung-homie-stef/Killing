using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputEvents
{
    public event Action<bool> onInventoryTogglePressed;
    public event Action onExitTogglePressed;
    public event Action<bool> onPauseTogglePressed;

    private bool _inventoryFlag = false;
    private bool _pauseFlag = false;

    public void InventoryTogglePressed()
    {
        if (onInventoryTogglePressed != null)
            _inventoryFlag = !_inventoryFlag;

        onInventoryTogglePressed(_inventoryFlag);
    }

    public void ExitTogglePressed()
    {
        if (onExitTogglePressed != null)
            onExitTogglePressed();
    }

    public void PauseTogglePressed()
    {
        if (onPauseTogglePressed != null)
            _pauseFlag = !_pauseFlag;

            onPauseTogglePressed(_pauseFlag);
    }
}
