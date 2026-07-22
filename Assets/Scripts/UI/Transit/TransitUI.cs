using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TransitUI : MonoBehaviour
{
    [System.Serializable]
    public struct BusStop
    {
        public string stopName;
        public RectTransform stopMapIconLocation;
        public Sprite stopPreviewThumbnailImage;
    }

    [Header("Bus Stop List Components")]
    [SerializeField] private List<BusStop> _busStopList;
    [SerializeField] private VerticalLayoutGroup _verticalLayoutGroup;
    [SerializeField] private GameObject _busStopListButtonPrefab;
    [SerializeField] private CanvasGroup _busStopCanvasGroup;
    [SerializeField] private TextMeshProUGUI _stopName;
    [SerializeField] private TextMeshProUGUI _stopArea;

    [Header("Visual Map Components")]
    [SerializeField] private GameObject _content;
    [SerializeField] private RectTransform _busStopLocationIcon;
    [SerializeField] private Image _locationPreviewThumbnail;

    [Header("Travel Prompt")]
    [SerializeField] private GameObject _travelPromptPanel;
    [SerializeField] private TextMeshProUGUI _travelPromptText;

    public void TransitToggle(bool flag)
    {
        if (flag)
        {
            ShowOrHideTransitUI(true, CursorLockMode.None);
            GameEventsManager.instance.playerEvents.DisablePlayerMovement();
        }
        else
        {
            ShowOrHideTransitUI(true, CursorLockMode.Locked);
            GameEventsManager.instance.playerEvents.EnablePlayerMovement();
        }
    }

    public void InitializeBusStop(string name, string area)
    {
        _stopName.text = name;
        _stopArea.text = area;

        for (int i = 0; i < _busStopList.Count; i++)
            GenerateBusStopListButtons(_busStopList[i]);
    }

    private void ShowOrHideTransitUI(bool flag, CursorLockMode mode)
    {
        Cursor.lockState = mode;
        Cursor.visible = flag;
        _content.SetActive(flag);
    }

    void GenerateBusStopListButtons(BusStop stopInfo)
    {
        BusStopButton _busStopButton = Instantiate(_busStopListButtonPrefab, _verticalLayoutGroup.transform).GetComponent<BusStopButton>();
        _busStopButton.Initialize(stopInfo);
    }

    public void UpdateMapInformation(RectTransform rTrans, Sprite sprite)
    {
        _busStopLocationIcon.anchoredPosition = rTrans.anchoredPosition;
        _locationPreviewThumbnail.sprite = sprite;
    }

    public void PopUpTravelPrompt(BusStopButton btn)
    {
        _travelPromptPanel.SetActive(true);
        _travelPromptText.text = "Travel to " + btn.GetStopName() + " ?";
        _busStopCanvasGroup.blocksRaycasts = false;
    }

    public void TravelPromptResponse(bool flag)
    {
        if (flag)
        {
            _content.SetActive(false);
        }
            _travelPromptPanel.SetActive(false);
            _busStopCanvasGroup.blocksRaycasts = true;
        
    }
}
