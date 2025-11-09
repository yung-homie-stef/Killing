using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvents 
{
    public event Action onDisablePlayerMovement;
    public event Action onEnablePlayerMovement;
    public event Action onBeginPlayerTeleportation;
    public event Action onPlayerTeleportation;
    public event Action<LocationTrigger> onPlayerEnterAreaBox;

    public void DisablePlayerMovement()
    {
        if (onDisablePlayerMovement != null)
            onDisablePlayerMovement();
    }

    public void EnablePlayerMovement()
    {
        if (onEnablePlayerMovement != null)
            onEnablePlayerMovement();
    }

    public void BeginPlayerTeleportation()
    {
        if (onBeginPlayerTeleportation != null)
            onBeginPlayerTeleportation();

        DisablePlayerMovement();
    }

    public void TeleportPlayer()
    {
        if (onPlayerTeleportation != null)
            onPlayerTeleportation();
    }

    public void PlayerEnterAreaBox(LocationTrigger triggerArea)
    {
        if (onPlayerEnterAreaBox != null)
            onPlayerEnterAreaBox(triggerArea);
    }
}
