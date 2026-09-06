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

        Debug.Log("cutscene is playing");
    }

    public void OnCutsceneEnd()
    {
        if (onCutsceneEnd != null)
            onCutsceneEnd();

        Debug.Log("cutscene is finished");
    }
}
