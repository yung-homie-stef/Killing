using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.UI;

public class CustomTweening : MonoBehaviour
{
    public enum TweeningDirection
    {
        Top,
        Bottom,
        Left,
        Right
    }

    [Header("UI Element Setup")]
    [SerializeField] private GameObject _UIElement;

    [SerializeField] private CanvasGroup _canvasGroup;

    [Header("Animation Setup")]
    [SerializeField] private TweeningDirection _openingTweenDirection;
    [SerializeField] private TweeningDirection _closingTweenDirection;
    [Space]
    [SerializeField] private Vector2 _tweeningDistance = new Vector2(100, 100);
    [SerializeField] private AnimationCurve _openingAnimationCurve = AnimationCurve.EaseInOut(0,0,1,1);
    [SerializeField] private AnimationCurve _closingAnimationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    [Range(0, 1f)][SerializeField] private float _openingAnimationDuration = 0.5f;
    [Range(0, 1f)][SerializeField] private float _closingAnimationDuration = 0.5f;

    private bool _isOpen = false;
    private Vector2 _initialPos;
    private Vector2 _currentPos;

    private Vector2 _upOffset;
    private Vector2 _downOffset;
    private Vector2 _leftOffset;
    private Vector2 _rightOffset;

    private Coroutine _tweeningCoroutine;


    private void OnValidate()
    {
         _canvasGroup = GetComponentInChildren<CanvasGroup>();

        _tweeningDistance.x = Mathf.Max(0, _tweeningDistance.x);
        _tweeningDistance.y = Mathf.Max(0, _tweeningDistance.y);
    }

    private void Start()
    {
        _initialPos = _UIElement.transform.position;
        InitializeOffsetPosition();
    }

    private void InitializeOffsetPosition()
    {
        _upOffset = new Vector2(0, _tweeningDistance.y);
        _downOffset = new Vector2(0, -_tweeningDistance.y);
        _leftOffset = new Vector2(-_tweeningDistance.x, 0);
        _rightOffset = new Vector2(_tweeningDistance.x, 0);
    }

    [ContextMenu("Toggle Open and Close")]
    public void ToggleOpenClose()
    {
        if (_isOpen)
            CloseWindow();
        else
            OpenWindow();
    }

    [ContextMenu("Open Window")]
    public void OpenWindow()
    {
        if (_isOpen)
            return;

        _isOpen = true;

        if (_tweeningCoroutine != null)
            StopCoroutine(_tweeningCoroutine);

        _tweeningCoroutine = StartCoroutine(TweenUIElement(true));
    }

    [ContextMenu("Close Window")]
    public void CloseWindow()
    {
        if (!_isOpen)
            return;

        _isOpen = false;

        if (_tweeningCoroutine != null)
            StopCoroutine(_tweeningCoroutine);

        _tweeningCoroutine = StartCoroutine(TweenUIElement(false));
    }

    private Vector2 GetOffset(TweeningDirection dir)
    {
        switch(dir)
        {
            case TweeningDirection.Left:
                return _leftOffset;
            case TweeningDirection.Right:
                return _rightOffset;
            case TweeningDirection.Top:
                return _upOffset;
            case TweeningDirection.Bottom:
                return _downOffset;
            default:
                return Vector2.zero;
        }
    }

    private IEnumerator TweenUIElement(bool open)
    {
        if (open)
            _UIElement.gameObject.SetActive(true);

        _currentPos = _UIElement.transform.position;
        Vector2 _targetPos = _currentPos;
        float _elapsedTime = 0;
        float _animationTime = 0;
        AnimationCurve _curve = null;

        if (open)
        {
            _targetPos = _currentPos + GetOffset(_openingTweenDirection);
            _animationTime = _openingAnimationDuration;
            _curve = _openingAnimationCurve;
        }
        else
        {
            _targetPos = _currentPos + GetOffset(_closingTweenDirection);
            _animationTime = _closingAnimationDuration;
            _curve = _closingAnimationCurve;
        }

        while (_elapsedTime < _animationTime)
        {
            float _evaluationAtTime = _curve.Evaluate(_elapsedTime / _animationTime);
            _UIElement.transform.position = Vector2.Lerp(_currentPos, _targetPos, _evaluationAtTime);
            _canvasGroup.alpha = open
                ? Mathf.Lerp(0f, 1f, _evaluationAtTime)
                : Mathf.Lerp(1f, 0f, _evaluationAtTime);

            _elapsedTime += Time.deltaTime;
            yield return null;
        }

        _UIElement.transform.position = _targetPos;
        _canvasGroup.alpha = open ? 1 : 0;
        _canvasGroup.interactable = open;
        _canvasGroup.blocksRaycasts = open;

        if (!open)
        {
            _UIElement.gameObject.SetActive(false);
            _UIElement.transform.position = _initialPos;
        }

    }

}
