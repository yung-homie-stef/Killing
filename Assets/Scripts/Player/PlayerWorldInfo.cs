using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerWorldInfo
{
    private static Transform _playerTeleportToLocation = null;
    private static string _locationName = "";

    public static void SetWorldInfo(Transform to, string name)
    {
        _playerTeleportToLocation = to;
        _locationName = name;
    }

    public static Transform GetTeleportToLocation()
    {
        return _playerTeleportToLocation;
    }

    public static string GetLocationName()
    {
        return _locationName;
    }

    public static void ResetPlayerWorldInfo()
    {
        _playerTeleportToLocation = null;
        _locationName = "";
    }
}
