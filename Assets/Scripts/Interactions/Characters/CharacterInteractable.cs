using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteractable : Interactable
{
    [SerializeField] private DialogueSystemTrigger _DS_Trigger;

    public override void Interact()
    {
        base.Interact();
        _DS_Trigger.OnUse();
        GameEventsManager.instance.playerEvents.DisablePlayerMovement();
    }
}
