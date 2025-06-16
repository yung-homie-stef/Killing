using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteractable : Interactable
{
    [Header("Character Parameters")]
    [SerializeField] protected bool _canSpeak = true;
    [SerializeField] protected string _characterName = "";
    protected CinemachineVirtualCamera _cinemachineVirtualCamera = null;

    [Header("Conversation Parameters")]
    [SerializeField]
    protected Conversation[] _conversations;
    [SerializeField]
    protected int _conversationIndex = 0;

    public virtual void Start()
    {
        _cinemachineVirtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();
    }

    public virtual void Disengage()
    {
        UIManager.instance.focusUI.SetCanFocus(true);
        _cinemachineVirtualCamera.Priority = 0;
        StartCoroutine(UIManager.instance.player.SetPlayerControls(0.2f, true));
    }

    public virtual void IncreaseConversationIndex()
    {
        if (_conversationIndex < _conversations.Length - 1)
            _conversationIndex++;
        else
            Debug.LogWarning("Trying to access index in conversation array beyond bounds, so it has not increased.");
    }

    protected virtual void BeginDialogue()
    {
        _cinemachineVirtualCamera.Priority = 1;
        UIManager.instance.dialogue.StartDialogue(this);
        UIManager.instance.player.SetPlayerControls(false);
    }
}
