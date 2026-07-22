using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusStopInteractable : Interactable
{
    [SerializeField] private string _busStopName = string.Empty;
    [SerializeField] private string _busStopArea = string.Empty;

    public override void Interact()
    {
        base.Interact();
        UIManager.instance._transitMenu.TransitToggle(true);
        UIManager.instance._transitMenu.InitializeBusStop(_busStopName, _busStopArea);
    }
}
