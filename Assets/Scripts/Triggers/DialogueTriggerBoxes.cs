using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using PixelCrushers;

public class DialogueTriggerBoxes : MonoBehaviour
{
    public enum TriggerType
    {
        None,
        Cutscene,
        Conversation
    }

    [SerializeField] private DialogueSystemTrigger _DS_Trigger;
    [SerializeField] private bool _selfDestruct = false;
    [SerializeField] private bool _usesCamera = false;
    [SerializeField][ShowIf("_usesCamera")] private CinemachineCamera _cinemachineVirtualCamera;

    private void OnTriggerEnter(Collider other)
    {
        if (_cinemachineVirtualCamera != null)
            _cinemachineVirtualCamera.Priority = 1;

        _DS_Trigger.OnUse();
        GameEventsManager.instance.playerEvents.DisablePlayerMovement();

        if (_selfDestruct)
            Destroy(this.gameObject);
    }
}
