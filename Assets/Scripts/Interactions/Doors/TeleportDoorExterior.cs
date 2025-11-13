using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportDoorExterior : Door
{
   [Header("Teleportation Variables")]
   [SerializeField] private Transform _teleportToLocation = null;
   [SerializeField] private string _teleportLocationName = "";
   [SerializeField] private GameObject _teleportLocationPrefab = null;

    public override void Interact()
    {
        base.Interact();
        GameEventsManager.instance.playerEvents.BeginPlayerTeleportation();
        PlayerWorldInfo.SetTeleportLocation(_teleportToLocation, _teleportLocationName);
        CityLoadManager.instance.SetEstablishmentPrefab(_teleportLocationPrefab);
        StartCoroutine(Teleport());
    }

    private IEnumerator Teleport()
    {
        yield return new WaitForSeconds(2.0f);
        LoseFocus();
        GameEventsManager.instance.playerEvents.TeleportPlayer();
        GameEventsManager.instance.playerEvents.EnablePlayerMovement();
        UIManager.instance.focusUI.SetCanFocus(true);
    }
}
