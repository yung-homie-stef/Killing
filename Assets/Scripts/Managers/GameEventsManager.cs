using System;
using UnityEngine;

public class GameEventsManager : MonoBehaviour
{
    public static GameEventsManager instance;
    public PlayerEvents playerEvents;
    public InputEvents inputEvents;

    private void Awake()
    {
        if (instance != null)
            Debug.LogError("Found more than one Game Events Manager in the scene.");
        instance = this;

        playerEvents = new PlayerEvents();
        inputEvents = new InputEvents();
    }
}
