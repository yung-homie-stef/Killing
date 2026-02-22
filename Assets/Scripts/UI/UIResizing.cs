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

    public void OnPointerClick(PointerEventData eventData)
    {
       UpdateDimensionsManually();
    }

    public void UpdateDimensionsManually()
    {
        _preferredSize = _textBox.GetPreferredValues().y;
        Sequence.Create(Tween.UISizeDelta(target: _UIPanel, endValue: new(_UIPanel.sizeDelta.x, _preferredSize), duration: 0.15f));
    }


}
