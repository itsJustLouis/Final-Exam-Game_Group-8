using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DIalogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void StartTutorial()
    {
        FindObjectOfType<DialogueManager>().StartTutorial(dialogue);
    }
}
