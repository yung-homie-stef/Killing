using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationTrigger : MonoBehaviour
{
    public enum LocationTriggerType
    {
        CityLocation,
        EstablishmentLocation
    }

    [SerializeField] private LocationTriggerType _triggerType;
    [SerializeField] private string _locationName = "";

    private void OnTriggerEnter(Collider other)
    {
        PlayerWorldInfo.SetLocationName(this);
        GameEventsManager.instance.playerEvents.PlayerEnterAreaBox(this, true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (_triggerType == LocationTriggerType.EstablishmentLocation)
        GameEventsManager.instance.playerEvents.PlayerEnterAreaBox(this, false);
    }

    public LocationTriggerType GetTriggerType()
    {
        return _triggerType;
    }

    public string GetLocationName()
    {
        return _locationName;
    }

}
