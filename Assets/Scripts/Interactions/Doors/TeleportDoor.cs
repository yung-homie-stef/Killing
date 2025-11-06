using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportDoor : Door
{
    public override void Interact()
    {
        base.Interact();
        GameEventsManager.instance.playerEvents.DisablePlayerMovement();
        GameEventsManager.instance.playerEvents.BeginPlayerTeleportation();
        StartCoroutine(PostTeleport());
    }

    private IEnumerator PostTeleport()
    {
        yield return new WaitForSeconds(1);
        GameEventsManager.instance.playerEvents.EnablePlayerMovement();
        GameEventsManager.instance.playerEvents.FinishPlayerTeleportation();
        UIManager.instance.focusUI.SetCanFocus(true);
    }
}
