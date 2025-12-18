using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class CharacterDialogue : Interactable
{
    private DialogueSystemTrigger _DS_Trigger;

    public override void Awake()
    {
        base.Awake();
        _DS_Trigger = GetComponent<DialogueSystemTrigger>();
    }

    public override void Interact()
    {
        _DS_Trigger.OnUse();
    }
}
