using PixelCrushers.DialogueSystem;
using PixelCrushers.DialogueSystem.Wrappers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTypewriterAudioForActor : MonoBehaviour
{
    void OnConversationLine(Subtitle subtitle)
    {
        Debug.Log("this script is firing off");

        PixelCrushers.DialogueSystem.DialogueActor dialogueActor;
        var panel = DialogueManager.standardDialogueUI.conversationUIElements.standardSubtitleControls.GetPanel(subtitle, out dialogueActor);
        var audibleActor = dialogueActor as AudibleDialogueActor;
        var typewriter = panel.GetTypewriter();
        typewriter.audioClip = audibleActor.typewriterAudioClip;
        typewriter.alternateAudioClips = audibleActor.alternateTypewriterAudioClips;
    }
}
