using PrimeTween;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject _contentParent;
    private CanvasGroup _buttonCanvasGroup = null;
    private CanvasGroup _currentPauseMenuScreen = null;

    [SerializeField] private PauseButton[] _pauseMenuButtons = new PauseButton[6];

    private void Awake()
    {
        _buttonCanvasGroup = _contentParent.GetComponentInChildren<CanvasGroup>();

        for (int i =0; i < _pauseMenuButtons.Length; i++)
            _pauseMenuButtons[i] = _buttonCanvasGroup.transform.GetChild(i).GetComponent<PauseButton>();

    }

    private void OnEnable()
    {
        GameEventsManager.instance.inputEvents.onPauseTogglePressed += PauseButtonToggle;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.inputEvents.onPauseTogglePressed -= PauseButtonToggle;
    }

    private void PauseButtonToggle(bool flag)
    {
        if (flag)
        {
            ShowOrHidePauseUI(true, CursorLockMode.None);
            GameEventsManager.instance.playerEvents.DisablePlayerMovement();           
        }
        else
        {
            ShowOrHidePauseUI(false, CursorLockMode.Locked);
            GameEventsManager.instance.playerEvents.EnablePlayerMovement();
        }
    }

    private void ShowOrHidePauseUI(bool flag, CursorLockMode mode)
    {
        Cursor.lockState = mode;
        Cursor.visible = flag;

        for (int i = 0; i < _pauseMenuButtons.Length; i++)
        {
            Tween.Scale(target: _pauseMenuButtons[i].transform, endValue: flag ? 1 : 0, duration: 0.45f);
            _pauseMenuButtons[i].enabled = flag;
        }

        _contentParent.SetActive(flag);
    }

    public void OpenPauseSubMenu(CanvasGroup group)
    {
        for (int i = 0; i < _pauseMenuButtons.Length; i++)
        {
            _pauseMenuButtons[i].enabled = false;
            Tween.Scale(target: _pauseMenuButtons[i].transform, startValue: _pauseMenuButtons[i].transform.localScale, endValue: Vector2.zero, duration: 0.15f);
        }

        _currentPauseMenuScreen = group;
        Tween.Scale(target: _currentPauseMenuScreen.transform, startValue: 0, endValue: 1, duration: 0.35f);
        
    }
}
