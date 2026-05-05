using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityLoadManager : MonoBehaviour
{
    public static CityLoadManager instance;
    public bool _cityLoadedIn = true;
    [SerializeField] private GameObject _establishmentInteriorPrefab = null;
    [SerializeField] private GameObject _cityContents = null;
    [SerializeField] private Transform _teleportLocation = null;

    private void Awake()
    {
        if (instance != null)
            Debug.LogError("Found more than one Game Events Manager in the scene.");
        instance = this;

        GameEventsManager.instance.playerEvents.onPlayerTeleportation += LoadUnloadCity;
    }

    public void PrepareTeleportation(GameObject prefab, Transform tPos)
    {
        _establishmentInteriorPrefab = prefab;
        _teleportLocation = tPos;
    }

    public Transform GetTeleportLocation()
    {
        return _teleportLocation;
    }

    private void LoadUnloadCity()
    {
        _cityLoadedIn = !_cityLoadedIn;

        if (_cityLoadedIn)
        {
            _cityContents.SetActive(true);

            if (_establishmentInteriorPrefab != null)
            {
                _establishmentInteriorPrefab.SetActive(false);
                _establishmentInteriorPrefab = null;
            }
        }
        else
        {
            _establishmentInteriorPrefab.SetActive(true);
            _cityContents.SetActive(false);
            
        }
    }

}
