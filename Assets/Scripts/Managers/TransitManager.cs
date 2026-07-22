using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitManager : MonoBehaviour
{
    [System.Serializable]
    public struct TeleportationPoint
    {
        public string _teleportationID;
        public Transform _teleportationTransform;
    }

    public static TransitManager instance;
    [SerializeField] private List<TeleportationPoint> _teleportationPoints;
    private Transform _targetDestination = null;

    private void Awake()
    {
        if (instance != null)
            Debug.LogError("Found more than one Transit Manager in the scene.");
        instance = this;
    }

    public void PrepareTeleportation(string stopName)
    {
        for (int i =0; i < _teleportationPoints.Count; i++)
        {
            if (_teleportationPoints[i]._teleportationID == stopName)
            {
                _targetDestination = _teleportationPoints[i]._teleportationTransform;
                Debug.Log("stop found");
                break;
            }
        }
    }

    public Transform GetFastTravelLocation()
    {
        return _targetDestination;
    }
}
