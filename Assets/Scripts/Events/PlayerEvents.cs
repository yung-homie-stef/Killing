using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvents 
{
    public event Action onDisablePlayerMovement;
    public event Action onEnablePlayerMovement;
    public event Action onBeginPlayerTeleportation;
    public event Action onFinishPlayerTeleportation;

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
    }

    public void FinishPlayerTeleportation()
    {
        if (onFinishPlayerTeleportation != null)
            onFinishPlayerTeleportation();
    }
}
