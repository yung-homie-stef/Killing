using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrackerAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private TextMeshProTypewriterEffect _typewriter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        _animator.SetTrigger("NewEntry");
    }
}
