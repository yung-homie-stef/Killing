using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportDoor : Door
{
    [Header("Teleportation Variables")]
    [SerializeField] private bool _isExterior = true;
    [SerializeField] private Transform _teleportToLocation = null;
    [SerializeField] private GameObject _teleportLocationPrefab = null;
    //[SerializeField] private string _teleportName = string.Empty;

    public override void Interact()
    {
        base.Interact();
        GameEventsManager.instance.playerEvents.BeginPlayerTeleportation(_isExterior);
        CityLoadManager.instance.PrepareTeleportation(_teleportLocationPrefab, _teleportToLocation);
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
