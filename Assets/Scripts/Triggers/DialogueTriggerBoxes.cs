using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using PixelCrushers.DialogueSystem;
using UnityEngine;

public class DialogueTriggerBoxes : MonoBehaviour
{
    public enum TriggerType
    {
        None,
        Cutscene,
        Conversation
    }

    [SerializeField] private TriggerType _type;
    [SerializeField] private DialogueSystemTrigger _DS_Trigger;
    [SerializeField] private bool _selfDestruct = false;
    [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;

    private void OnTriggerEnter(Collider other)
    {
        switch (_type)
        {
            case TriggerType.None:
                break;
            case TriggerType.Cutscene:
                break;
            case TriggerType.Conversation:
                if (_cinemachineVirtualCamera != null)
                    _cinemachineVirtualCamera.Priority = 1;
                break;
        }

        _DS_Trigger.OnUse();
        GameEventsManager.instance.playerEvents.DisablePlayerMovement();

        if (_selfDestruct)
        Destroy(this.gameObject);
    }
}
