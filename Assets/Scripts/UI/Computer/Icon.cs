using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Icon : MonoBehaviour, IPointerClickHandler, IDragHandler
{
    private int tap = 0;
    private Canvas _canvas;
    private RectTransform _rectTransform;
    private Desktop _desktop;
    private TextMeshProUGUI _programNameText;

    [SerializeField] private Image _textHighlight;
    [SerializeField] private GameObject _program;
    [SerializeField] private string _programName;

    private void Start()
    {
        _canvas = GetComponentInParent<Canvas>();
        _rectTransform = GetComponent<RectTransform>();
        _desktop = GetComponentInParent<Desktop>();
        _programNameText = GetComponentInChildren<TextMeshProUGUI>();

        _programNameText.text = _programName;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        tap = eventData.clickCount;

        if (tap == 1)
        {
            _textHighlight.enabled = true;
            if (_desktop._highlightedText != this)
            {
                _desktop.UnhighlightText();
                _desktop.SetHighlightedText(_textHighlight);
            }
        }
        else if (tap == 2)
            OpenApp();
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
        TrapToScreen();
    }

    public virtual void OpenApp()
    {
        Debug.Log("opened " + _program.name);
        _program.SetActive(true);
    }

    private void TrapToScreen()
    {
        Vector3 _differenceMinimum = _rectTransform.position + (Vector3)_rectTransform.rect.position;
        Vector3 _differenceMaximum = (Vector3)Camera.main.pixelRect.size - _rectTransform.position + (Vector3)_rectTransform.rect.position;

        if (_differenceMinimum.x < 10)
            _rectTransform.position -= new Vector3(_differenceMinimum.x, 0.0f, 0.0f);
        if (_differenceMinimum.y < 10)
            _rectTransform.position -= new Vector3(0.0f, _differenceMinimum.y, 0.0f);
        if (_differenceMaximum.x < 10)
            _rectTransform.position += new Vector3(_differenceMaximum.x, 0.0f, 0.0f);
        if (_differenceMaximum.y < 10)
            _rectTransform.position += new Vector3(0.0f, _differenceMaximum.y, 0.0f);
    }


}
