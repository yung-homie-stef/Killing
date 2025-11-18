using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject _contentParent;

    private void OnEnable()
    {
        GameEventsManager.instance.inputEvents.onPauseTogglePressed += PauseButtonToggle;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.inputEvents.onPauseTogglePressed -= PauseButtonToggle;
    }

    private void PauseButtonToggle(bool flag)
    {
        if (flag)
        {
            ShowOrHidePauseUI(true, CursorLockMode.None);
            GameEventsManager.instance.playerEvents.DisablePlayerMovement();           
        }
        else
        {
            ShowOrHidePauseUI(false, CursorLockMode.Locked);
            GameEventsManager.instance.playerEvents.EnablePlayerMovement();
        }
    }

    private void ShowOrHidePauseUI(bool flag, CursorLockMode mode)
    {
        _contentParent.SetActive(flag);
        Cursor.lockState = mode;
        Cursor.visible = flag;
    }
}
