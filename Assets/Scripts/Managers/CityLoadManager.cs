using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityLoadManager : MonoBehaviour
{
    public static CityLoadManager instance;
    [SerializeField] private GameObject _establishmentInteriorPrefab = null;
    [SerializeField] private GameObject _cityContents = null;
    [SerializeField] private Transform _teleportLocation = null;

    private void Awake()
    {
        if (instance != null)
            Debug.LogError("Found more than one City Load Manager in the scene.");
        instance = this;

        GameEventsManager.instance.playerEvents.onPlayerEnterIndoorOutdoor += LoadUnloadCity;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.playerEvents.onPlayerEnterIndoorOutdoor -= LoadUnloadCity;
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

    private void LoadUnloadCity(bool exterior)
    {

        if (!exterior)
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
