using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCinemachinePriorityEvent : CinemachineCameraPriorityOnDialogueEvent
{
    [SerializeField] private bool _selfDestruct;

    public override void TryEndActions(Transform actor)
    {
        base.TryEndActions(actor);
        if (_selfDestruct)
            Destroy(gameObject);
    }


}
