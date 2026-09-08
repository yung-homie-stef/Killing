using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.Events;
using PixelCrushers;

[RequireComponent(typeof(DialogueSystemTrigger))]
public class DialogueTriggerBoxes : MonoBehaviour
{
    [SerializeField] private DialogueSystemTrigger _DS_Trigger;
    [SerializeField] private bool _selfDestruct = true;
    [SerializeField] private UnityEvent _onTriggerEnter;
    private Collider _collider;
    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _onTriggerEnter.Invoke();
        _DS_Trigger.OnUse();
        GameEventsManager.instance.playerEvents.DisablePlayerMovement();
        _collider.enabled = false;

        if (_selfDestruct)
            Destroy(gameObject);
    }

}
