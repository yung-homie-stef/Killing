using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PixelCrushers.DialogueSystem;

public class CustomTypewriterEffect : TextMeshProTypewriterEffect
{
    public override IEnumerator Play(int fromIndex = 0)
    {
        textComponent.text = textComponent.text.Replace("@", "...");
        base.Play(fromIndex);

        yield return typewriterCoroutine;
    }
}
