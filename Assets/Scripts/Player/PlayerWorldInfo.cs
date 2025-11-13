using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerWorldInfo
{
    private static Transform _playerTeleportToLocation = null;
    private static string _cityLocationName = "default";
    private static string _establishmentLocationName = "default";

    public static void SetTeleportLocation(Transform to, string name)
    {
        _playerTeleportToLocation = to;
        _cityLocationName = name;
    }

    public static void SetTeleportLocation(Transform to)
    {
        _playerTeleportToLocation = to;
    }

    public static void SetLocationName(LocationTrigger trigger)
    {
        if (trigger.GetTriggerType() == LocationTrigger.LocationTriggerType.CityLocation)
            _cityLocationName = trigger.GetLocationName();
        else if (trigger.GetTriggerType() == LocationTrigger.LocationTriggerType.EstablishmentLocation)
            _establishmentLocationName = trigger.GetLocationName();
    }

    public static Transform GetTeleportToLocation()
    {
        return _playerTeleportToLocation;
    }

    public static string GetCityLocationName()
    {
        return _cityLocationName;
    }

    public static string GetEstablishmentLocationName()
    {
        return _establishmentLocationName;
    }

    public static void ResetPlayerWorldInfo()
    {
        _playerTeleportToLocation = null;
        _cityLocationName = "";
        _establishmentLocationName = "";
    }
}
