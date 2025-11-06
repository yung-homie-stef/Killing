using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportDoor : Door
{
    public override void Interact()
    {
        base.Interact();
        Debug.Log("teleporting to this zone");
    }
}
