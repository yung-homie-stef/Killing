using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FocusUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _focusLabel;

    private bool _canFocus = true;

    // Start is called before the first frame update
    void Start()
    {
        _focusLabel.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateFocusUI(Interactable interactable)
    {
        if (_canFocus)
        _focusLabel.text = interactable.label;
    }

    public void ClearFocusUI()
    {
        _focusLabel.text = "";
    }

    public void SetCanFocus(bool flag)
    {
        _canFocus = flag;
    }    
}
