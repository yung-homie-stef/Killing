using PixelCrushers.DialogueSystem;
using PrimeTween;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueBoxResizing : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private RectTransform _UIPanel;
    private float _preferredSize;
    [SerializeField] private TextMeshProUGUI _textBox;
    [SerializeField] private float _preferredHeightAddition = 50.0f;
    [SerializeField] private RectTransform _underlineImage = null;

    void OnEnable()
    {
        UpdateDimensionsManually();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // TODO: FIX! SHOULD NOT BE EVERY SINGLE TIME I CLICK
       UpdateDimensionsManually();
    }

    public void UpdateDimensionsManually()
    {
        _preferredSize = _textBox.GetPreferredValues().y + _preferredHeightAddition;

        if (_preferredSize != _textBox.GetPreferredValues().y)
        Sequence.Create(Tween.UISizeDelta(target: _UIPanel, endValue: new(_UIPanel.sizeDelta.x, _preferredSize), duration: 0.15f));
    }

    public void UnderlineAnimation(bool flag)
    {
        if (flag)
            Tween.UISizeDelta(target: _underlineImage, startValue: new(0.0f, 25.0f), endValue: new(600.0f, 25.0f), duration: 0.25f);
        else
            Tween.UISizeDelta(target: _underlineImage, endValue: new(0.0f, 25.0f), duration: 0.25f);
        //_underlineImage.sizeDelta = new Vector2(0.0f, 0.25f);

    }


}
