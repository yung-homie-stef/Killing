using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;
using Cinemachine;
using System;

public class CharacterDialogue : Interactable
{
    [Header("Character Parameters")]
    [SerializeField] private bool _canSpeak = true;
    [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;
    private DialogueSystemTrigger _DS_Trigger;

    public override void Awake()
    {
        base.Awake();
        _DS_Trigger = GetComponent<DialogueSystemTrigger>();
        _cinemachineVirtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();
        DialogueManager.instance.conversationEnded += Disengage;
    }

    public override void Interact()
    {
        if (_canSpeak)
        {
            base.Interact();
            _DS_Trigger.OnUse();
            _cinemachineVirtualCamera.Priority = 1;
        }
    }

    public void SetCanSpeak(bool flag)
    {
        _canSpeak = flag;
    }

    private void Disengage(Transform t)
    {
        _cinemachineVirtualCamera.Priority = 0;
    }
}
