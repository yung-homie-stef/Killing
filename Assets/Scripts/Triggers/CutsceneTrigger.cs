using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class CutsceneTrigger : MonoBehaviour
{
    [SerializeField] private TimelineAsset _cutscene;
    private Collider _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        CutsceneManager.instance.InitializeCutscene(_cutscene);
        _collider.enabled = false;
    }
}
