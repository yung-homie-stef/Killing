using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class CutsceneManager : MonoBehaviour
{
    private PlayableDirector _cutscenePlayer;
    public static CutsceneManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;
    }

    private void OnEnable()
    {
        _cutscenePlayer = GetComponent<PlayableDirector>();
        _cutscenePlayer.played += OnCutsceneStart;
        _cutscenePlayer.stopped += OnCutsceneEnd;
    }

    private void OnDisable()
    {
        _cutscenePlayer.played -= OnCutsceneStart;
        _cutscenePlayer.stopped -= OnCutsceneEnd;
    }

    public void InitializeCutscene(TimelineAsset cutscene)
    {
        _cutscenePlayer.playableAsset = cutscene;
        _cutscenePlayer.Play(cutscene);
    }

    void OnCutsceneStart(PlayableDirector director)
    {
        if (_cutscenePlayer == director)
            GameEventsManager.instance.cutsceneEvents.OnCutsceneBegin();
    }

    void OnCutsceneEnd(PlayableDirector director)
    {
        if (_cutscenePlayer == director)
            GameEventsManager.instance.cutsceneEvents.OnCutsceneEnd();
    }

    public void StartCutsceneFromDialogue(TimelineAsset cutscene)
    {
        InitializeCutscene(cutscene);
    }
}
