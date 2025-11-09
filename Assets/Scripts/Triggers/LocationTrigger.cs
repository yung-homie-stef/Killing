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
        GameEventsManager.instance.playerEvents.PlayerEnterAreaBox(this);
    }

    private void OnTriggerExit(Collider other)
    {
        if (_triggerType == LocationTriggerType.EstablishmentLocation)
            Debug.Log("swag");
            //
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
