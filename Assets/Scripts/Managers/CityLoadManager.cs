using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityLoadManager : MonoBehaviour
{
    public static CityLoadManager instance;
    public bool _cityLoadedIn = true;
    private GameObject _establishmentInteriorPrefab = null;
    [SerializeField] private GameObject _cityContents = null;

    private void Awake()
    {
        if (instance != null)
            Debug.LogError("Found more than one Game Events Manager in the scene.");
        instance = this;

        GameEventsManager.instance.playerEvents.onPlayerTeleportation += LoadUnloadCity;
    }

    public void SetEstablishmentPrefab(GameObject prefab)
    {
        _establishmentInteriorPrefab = prefab;
    }

    private void LoadUnloadCity()
    {
        _cityLoadedIn = !_cityLoadedIn;

        if (_cityLoadedIn)
        {
            _cityContents.SetActive(true);
            _establishmentInteriorPrefab.SetActive(false);
            _establishmentInteriorPrefab = null;
        }
        else
        {
            _cityContents.SetActive(false);
            _establishmentInteriorPrefab.SetActive(true);
        }
    }

}
