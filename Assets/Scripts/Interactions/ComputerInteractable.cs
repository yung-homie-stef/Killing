using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerInteractable : Interactable
{
    [SerializeField] private GameObject _desktopVisuals;

    public override void Interact()
    {
        base.Interact();

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        GameEventsManager.instance.playerEvents.DisablePlayerMovement();
        Instantiate(_desktopVisuals, UIManager.instance.transform);
    }

}
