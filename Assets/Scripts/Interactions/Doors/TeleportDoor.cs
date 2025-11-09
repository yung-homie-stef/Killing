using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportDoor : Door
{
   [Header("Teleportation Variables")]
   [SerializeField] private Transform _teleportToLocation = null;
   [SerializeField] private string _teleportLocationName = "";

    public override void Interact()
    {
        base.Interact();
        GameEventsManager.instance.playerEvents.BeginPlayerTeleportation();
        PlayerWorldInfo.SetTeleportLocation(_teleportToLocation, _teleportLocationName);
        StartCoroutine(Teleport());
    }

    private IEnumerator Teleport()
    {
        yield return new WaitForSeconds(2.0f);
        LoseFocus();
        GameEventsManager.instance.playerEvents.TeleportPlayer();
        yield return new WaitForSeconds(1.5f);
        GameEventsManager.instance.playerEvents.EnablePlayerMovement();
        UIManager.instance.focusUI.SetCanFocus(true);
    }
}
