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

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private TutorialManager _tutorialManager; 

    private Queue<Sentence> _sentences;

    [SerializeField] private GameObject _dialogueBox;
    [SerializeField] private TMP_Text _dialogueText;
    [SerializeField] private TMP_Text _objectiveText;

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
        if (!_dialogueBox || !_dialogueText || !_objectiveText)
        {
            _dialogueBox = GameObject.Find("DialogueBox");
            _dialogueText = GameObject.Find("DialogueText").GetComponent<TMP_Text>();
            _objectiveText = GameObject.Find("ObjectiveText").GetComponent<TMP_Text>();
        }

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
            _tutorialManager.AccelerationTutorial();
        }
        else if (_currentDialogue.unlockStrafing)
        {
            _tutorialManager.StrafingTutorial();
        }
        else if (_currentDialogue.unlockBlackhole)
        {
            _tutorialManager.BlackHoleTutorial();
        }
        else if (_currentDialogue.unlockShooting)
        {
            _tutorialManager.ShootingTutorial();
        }
        else if (_currentDialogue.endTutorial)
        {
            _tutorialManager.EndTutorial();
        }

        _objectiveText.text = _currentDialogue.objectiveText;

        _dialogueBox.SetActive(false);
    }
}
