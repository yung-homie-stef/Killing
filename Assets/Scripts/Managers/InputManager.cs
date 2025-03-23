using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    public void QuestLogTogglePressed(InputAction.CallbackContext context)
    {
        Debug.Log("Q HAS BEEN PRESSED");

        if (context.started)
            GameEventsManager.instance.inputEvents.QuestLogTogglePressed();
    }
}
