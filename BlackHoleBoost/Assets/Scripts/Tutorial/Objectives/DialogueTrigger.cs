using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [11/02/2024]
 * [triggers the dialogue to play]
 */

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    /// <summary>
    /// starts dialogue sequence
    /// </summary>
    public void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(dialogue);
    }
}
