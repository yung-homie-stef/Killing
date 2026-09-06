using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneEvents
{
    public event Action onCutsceneBegin;
    public event Action onCutsceneEnd;

    public void OnCutsceneBegin()
    {
        if (onCutsceneBegin != null)
            onCutsceneBegin();
    }

    public void OnCutsceneEnd()
    {
        if (onCutsceneBegin != null)
            onCutsceneEnd();
    }
}
