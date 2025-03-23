using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Program : MonoBehaviour, IDragHandler
{
    private Canvas _canvas;
    private RectTransform _rectTransform;
    [SerializeField] private bool _deletesWhenClosed = false;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _programText;
    [SerializeField] private string _programName;

    public virtual void Start()
    {
        _canvas = GetComponentInParent<Canvas>();
        _rectTransform = GetComponent<RectTransform>();
        _programText.text = _programName;
    }

    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
        TrapToScreen();
    }

    public void CloseProgram()
    {
        if (!_deletesWhenClosed)
            gameObject.SetActive(false);
        else
            Destroy(gameObject);
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
