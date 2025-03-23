using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteractableDialogue : CharacterInteractable
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

   public override void Interact()
   {
       base.Interact();

       if (_canSpeak)
       {
            BeginDialogue();
       }

   }

    private void BeginDialogue()
    {
        _cinemachineVirtualCamera.Priority = 1;
        UIManager.instance.dialogue.SetConversation(_conversations[0]);
        UIManager.instance.dialogue.StartDialogue(this);
        UIManager.instance.player.SetPlayerControls(false);
    }
}
