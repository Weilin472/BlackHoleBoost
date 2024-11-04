using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/*
 * Author: [Lam, Justin]
 * Last Updated: [11/03/2024]
 * [manages the dialogue for the tutorial]
 */

public class DialogueManager : Singleton<DialogueManager>
{
    private Queue<Sentence> _sentences;

    [SerializeField] private GameObject _dialogueBox;
    [SerializeField] private TMP_Text _dialogueText;

    private Dialogue _currentDialogue;
    private Sentence _currentSentence;

    /// <summary>
    /// initializes the queue
    /// </summary>
    private void OnEnable()
    {
        _sentences = new Queue<Sentence>();
    }

    /// <summary>
    /// starts dialogue
    /// </summary>
    /// <param name="dialogue">dialogue to display</param>
    public void StartDialogue(Dialogue dialogue)
    {
        _dialogueBox.SetActive(true);
        _currentDialogue = dialogue;
        _sentences.Clear();
        foreach (Sentence sentence in dialogue.sentences)
        {
            _sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    /// <summary>
    /// displays the next sentence for the dialogue
    /// </summary>
    public void DisplayNextSentence()
    {
        if (_currentSentence != null && _currentSentence.outline)
        {
            _currentSentence.highlight.Dehighlight();
        }

        if (_sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        _currentSentence = _sentences.Dequeue();

        _dialogueText.text = _currentSentence.sentence;

        //change later, dont have time
        if (_currentSentence.outline)
        {
            _currentSentence.highlight.Highlight();
        }
        
    }

    /// <summary>
    /// ends dialogue
    /// </summary>
    private void EndDialogue()
    {
        if (_currentDialogue.unlockAcceleration)
        {
            TutorialManager.Instance.UnlockAcceleration();
        }
        else if (_currentDialogue.unlockStrafing)
        {
            TutorialManager.Instance.UnlockStrafing();
        }
        else if (_currentDialogue.unlockBlackhole)
        {
            TutorialManager.Instance.UnlockBlackHole();
        }
        else if (_currentDialogue.unlockShooting)
        {
            TutorialManager.Instance.UnlockShooting();
        }

        _dialogueBox.SetActive(false);
    }
}
