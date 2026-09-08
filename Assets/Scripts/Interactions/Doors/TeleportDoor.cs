using PixelCrushers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportDoor : Door
{
    [Header("Teleportation Variables")]
    [SerializeField][Tooltip("Is this door accessed from outdoors (true) or from inside (false)")] private bool _isExterior = true;
    [SerializeField] private Transform _teleportToLocation = null;
    [SerializeField] private GameObject _teleportLocationPrefab = null;
    [SerializeField] private string _newLocationName = string.Empty;

    private void OnValidate()
    {
        if (_isExterior)
            type = InteractableType.EntryDoor;
        else
            type = InteractableType.ExitDoor;
    }

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
        UIManager.instance._hudMenu.SetLocationBannerText(_newLocationName);
    }
}
