using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [Header("Text Parameters")]
    private TextMeshProUGUI _dialogueTextBox;
    [SerializeField] private float _typingSpeed = 0.0f;
    [SerializeField] private int _currentCharacterCount = 0;
    [SerializeField] private Conversation _conversation;

    [SerializeField] private int _lineIndex = 0;
    private CharacterInteractable _speakingCharacter = null;
    private bool _isTalking = false;

    // Start is called before the first frame update
    void Start()
    {
        _dialogueTextBox = GetComponentInChildren<TextMeshProUGUI>();
        _dialogueTextBox.text = "";
    }

    private void Update()
    {
        if (_isTalking)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (_dialogueTextBox.maxVisibleCharacters >= _currentCharacterCount)
                    NextLine();
                else
                    AutoCompleteDialogue();
            }
        }
    }

    public void StartDialogue(CharacterInteractable character)
    {
        _lineIndex = 0;
        _speakingCharacter = character;

        StartCoroutine(TypeOutDialogue(_conversation.lines[_lineIndex]));
    }

    private IEnumerator TypeOutDialogue(string line)
    {
        _isTalking = true;
        _dialogueTextBox.text = line;
        _dialogueTextBox.maxVisibleCharacters = 0;

        _currentCharacterCount = line.Length;

        for (int i = 0; _dialogueTextBox.maxVisibleCharacters < _currentCharacterCount; i++)
        {
            _dialogueTextBox.maxVisibleCharacters++;
            yield return new WaitForSeconds(_typingSpeed);
        }            
    }

    private void NextLine()
    {
        _dialogueTextBox.text = "";

        if (_lineIndex < _conversation.lines.Length - 1)
        {
            _lineIndex++;
            StartCoroutine(TypeOutDialogue(_conversation.lines[_lineIndex]));
        }
        else
        {
            _isTalking = false;
            if (_conversation.canIncreaseIndex)
                _speakingCharacter.IncreaseConversationIndex();
            _speakingCharacter.Disengage();
        }
    }

    private void AutoCompleteDialogue()
    {
        StopAllCoroutines();
        _dialogueTextBox.maxVisibleCharacters = _currentCharacterCount;
    }

    public void SetConversation(Conversation convo)
    {
        _conversation = convo;
    }
}
