using PixelCrushers.DialogueSystem;
using PrimeTween;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIResizing : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private RectTransform _UIPanel;
    private float _preferredSize;
    [SerializeField] private TextMeshProUGUI _textBox;
    [SerializeField] private float _preferredHeightAddition = 50.0f;

    void OnEnable()
    {
        UpdateDimensionsManually();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // FIX SHOULD NOT BE EVERY SINGLE TIME I CLICK
       UpdateDimensionsManually();
    }

    public void UpdateDimensionsManually()
    {
        _preferredSize = _textBox.GetPreferredValues().y + _preferredHeightAddition;

        if (_preferredSize != _textBox.GetPreferredValues().y)
        Sequence.Create(Tween.UISizeDelta(target: _UIPanel, endValue: new(_UIPanel.sizeDelta.x, _preferredSize), duration: 0.15f));
    }


}
