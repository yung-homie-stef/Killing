using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvents 
{
    public event Action onDisablePlayerMovement;
    public event Action onEnablePlayerMovement;
    public event Action onBeginPlayerTeleportation;
    public event Action onPlayerFastTravel;
    public event Action<bool> onPlayerEnterIndoorOutdoor;

    public event Action<LocationTrigger, bool> onPlayerEnterAreaBox;
    private bool _isExterior = true;

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

    public void BeginPlayerTeleportation(bool isExterior)
    {
        if (onBeginPlayerTeleportation != null)
            onBeginPlayerTeleportation();

        DisablePlayerMovement();
        _isExterior = isExterior;

        Debug.Log(_isExterior); 
    }

    public void PlayerFastTravel()
    {
        if (onPlayerFastTravel != null)
            onPlayerFastTravel();
    }

    public void BeginPlayerTeleportation()
    {
        if (onBeginPlayerTeleportation != null)
            onBeginPlayerTeleportation();

        DisablePlayerMovement();
    }

    public void TeleportPlayer()
    {
        if (onPlayerEnterIndoorOutdoor != null)
            onPlayerEnterIndoorOutdoor(_isExterior);
    }

    public void PlayerEnterAreaBox(LocationTrigger triggerArea, bool flag)
    {
        if (onPlayerEnterAreaBox != null)
            onPlayerEnterAreaBox(triggerArea, flag);
    }

    public void PlayerFundsChange(int dollarAmount)
    {

    }

}
