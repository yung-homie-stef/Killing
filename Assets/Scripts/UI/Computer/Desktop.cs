using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Desktop : MonoBehaviour
{
    [SerializeField] private KeyCode _logOffKeyCode = KeyCode.Escape;
    [HideInInspector] public Image _highlightedText = null;

    private void Update()
    {
        if (Input.GetKeyDown(_logOffKeyCode))
            LogOff();
    }

    public void UnhighlightText()
    {
        if (_highlightedText != null)
        {
            _highlightedText.enabled = false;
            _highlightedText = null;
        }
    }

    public void SetHighlightedText(Image img)
    {
        _highlightedText = img;
    }

    private void LogOff()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        StartCoroutine(UIManager.instance.player.SetPlayerControls(0.2f, true));
    }
}
