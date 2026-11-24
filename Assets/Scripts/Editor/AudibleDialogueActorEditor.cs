using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;
namespace PixelCrushers.DialogueSystem
{
    [CustomEditor(typeof(AudibleDialogueActor), true)]
    [CanEditMultipleObjects]
    public class AudibleDialogueActorEditor : DialogueActorEditor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(AudibleDialogueActor.typewriterAudioClip)), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(AudibleDialogueActor.alternateTypewriterAudioClips)), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}
