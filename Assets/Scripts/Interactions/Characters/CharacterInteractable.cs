using Cinemachine;
using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteractable : Interactable
{
    [SerializeField] private DialogueSystemTrigger _DS_Trigger;
    //[SerializeField] private CinemachineVirtualCamera _characterVirtualCamera;

    public override void Awake()
    {
        DialogueManager.instance.conversationEnded += OnConversationEnded;
    }

    private void OnDisable()
    {
        DialogueManager.instance.conversationEnded -= OnConversationEnded;
    }

    public override void Interact()
    {
        base.Interact();
        _DS_Trigger.OnUse();
        //_characterVirtualCamera.Priority = 1;
        GameEventsManager.instance.playerEvents.DisablePlayerMovement();
    }

    private void OnConversationEnded(Transform t)
    {
        //_characterVirtualCamera.Priority = 0;
    }
}
