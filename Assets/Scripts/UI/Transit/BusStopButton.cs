using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static TransitUI;

public class BusStopButton : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private TextMeshProUGUI _name = null;
    [SerializeField] private RectTransform _iconLocation;
    [SerializeField] private Sprite _previewThumbnail;


    public void Initialize(BusStop info)
    {
        name = "Stop Button " + info.stopName;
        _name.text = info.stopName;
        _iconLocation = info.stopMapIconLocation;
        _previewThumbnail = info.stopPreviewThumbnailImage;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UIManager.instance._transitMenu.UpdateMapInformation(_iconLocation, _previewThumbnail);
    }

    public void InitiateTravelPrompt()
    {
        UIManager.instance._transitMenu.PopUpTravelPrompt(this);
    }

    public string GetStopName()
    {
        return _name.text;
    }
}
