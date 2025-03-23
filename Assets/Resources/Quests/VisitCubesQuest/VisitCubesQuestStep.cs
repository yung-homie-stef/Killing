using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VisitCubesQuestStep : QuestStep
{
    [Header("Config")]
    public string cubeNumber = "first";

    private void Start()
    {
        string status = "Visit the " + cubeNumber + " cube in this level";
        ChangeState("", status);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            string status = "The " + cubeNumber + " has been visited";
            ChangeState ("", status);
            FinishQuestStep();
        }
    }

}
