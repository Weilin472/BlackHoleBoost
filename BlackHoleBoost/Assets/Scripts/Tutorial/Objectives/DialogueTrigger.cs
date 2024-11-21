using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [11/20/2024]
 * [triggers the dialogue to play]
 */

public class DialogueTrigger : MonoBehaviour
{
    private DialogueManager _dialugueManager;

    public Dialogue dialogue;

    /// <summary>
    /// gets the dialogue manager
    /// </summary>
    private void Start()
    {
        _dialugueManager = GameObject.FindGameObjectWithTag("TutorialManager").GetComponent<DialogueManager>();
    }

    /// <summary>
    /// starts dialogue sequence
    /// </summary>
    public void TriggerDialogue()
    {
        _dialugueManager.StartDialogue(dialogue);
    }
}
