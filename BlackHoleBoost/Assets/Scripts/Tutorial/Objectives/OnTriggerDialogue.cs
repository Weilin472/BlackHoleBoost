using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [11/03/2024]
 * [triggers dialogue on trigger enter]
 */

public class OnTriggerDialogue : DialogueTrigger
{
    private void OnTriggerEnter(Collider other)
    {
        TriggerDialogue();
        gameObject.SetActive(false);
    }
}
