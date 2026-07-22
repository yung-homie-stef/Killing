using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitUI : MonoBehaviour
{
    [System.Serializable]
    public struct BusStop
    {
        public string stopName;
        public RectTransform stopMapIconLocation;
        public Image stopPreviewThumbnailImage;
    }

    [SerializeField] private List<BusStop> _busStopList;
    [SerializeField] private GameObject _busStopListButtonPrefab;
    [SerializeField] private VerticalLayoutGroup _vlg;
    [SerializeField] private RectTransform _busStopLocationIcon;

    // Start is called before the first frame update
    void Start()
    {
        for (int i =0; i < _busStopList.Count; i++)
            GenerateBusStopListButtons(_busStopList[i]);
    }

    void GenerateBusStopListButtons(BusStop stopInfo)
    {
        BusStopButton _busStopButton = Instantiate(_busStopListButtonPrefab, _vlg.transform).GetComponent<BusStopButton>();
        _busStopButton.Initialize(stopInfo);
    }

    public void UpdateMapInformation(RectTransform rTrans)
    {
        _busStopLocationIcon.anchoredPosition = rTrans.anchoredPosition;
    }
}
