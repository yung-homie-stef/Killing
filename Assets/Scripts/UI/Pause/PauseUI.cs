using PrimeTween;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject _content;
    private CanvasGroup _buttonCanvasGroup = null;
    private CanvasGroup _currentPauseMenuScreen = null;

    [SerializeField] private PauseButton[] _pauseMenuButtons = new PauseButton[6];

    private void Awake()
    {
        _buttonCanvasGroup = _content.GetComponentInChildren<CanvasGroup>();

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
        _content.SetActive(flag);

        for (int i = 0; i < _pauseMenuButtons.Length; i++)
        {
            Tween.Scale(target: _pauseMenuButtons[i].transform, endValue: flag ? 1 : 0, duration: 0.45f);
            _pauseMenuButtons[i].enabled = flag;
        }

        if (!flag)
            RemovePauseSubMenu();

    }

    public void OpenPauseSubMenu(CanvasGroup group)
    {
        _currentPauseMenuScreen = group;
        _currentPauseMenuScreen.gameObject.SetActive(true);

        Tween.Scale(target: _currentPauseMenuScreen.transform, startValue: 0, endValue: 1, duration: 0.35f, startDelay: 0.15f);

        for (int i = 0; i < _pauseMenuButtons.Length; i++)
        {
            _pauseMenuButtons[i].enabled = false;
            Tween.Scale(target: _pauseMenuButtons[i].transform, startValue: _pauseMenuButtons[i].transform.localScale, endValue: Vector2.zero, duration: 0.15f);
        }
    }

    public void RemovePauseSubMenu()
    {
        _currentPauseMenuScreen.gameObject.SetActive(false);
        _currentPauseMenuScreen.transform.localScale = Vector3.zero;
        _currentPauseMenuScreen = null;
    }
}
