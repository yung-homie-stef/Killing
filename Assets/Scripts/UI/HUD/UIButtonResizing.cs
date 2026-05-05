using PrimeTween;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonResizing : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform _rectTransform;
    private float _height;
    private float _defaultWidth;
    [SerializeField] private float _expandedWidth = 0.0f;

    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _height = _rectTransform.sizeDelta.y;
        _defaultWidth = _rectTransform.sizeDelta.x;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Tween.UISizeDelta(target: _rectTransform, endValue: new(_expandedWidth, _height), duration: 0.15f);
        Debug.Log("swag");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Tween.UISizeDelta(target: _rectTransform, startValue: _rectTransform.sizeDelta, endValue: new(_defaultWidth, _height), duration: 0.15f);
    }
}
