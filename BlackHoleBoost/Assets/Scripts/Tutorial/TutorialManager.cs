using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [11/03/2024]
 * [Manages the tutorial]
 */

public class TutorialManager : Singleton<TutorialManager>
{
    [SerializeField] private DialogueTrigger _dialogueTrigger;
    [SerializeField] private GameObject _playerPrefab;
    private GameObject _currentPlayer;
    private PlayerControl _currentPlayerControl;

    [SerializeField] private GameObject _accelerationObjective;

    [SerializeField] private GameObject _blackholeObjective;

    [SerializeField] private GameObject _strafeObjective;

    /// <summary>
    /// starts tutorial (temp)
    /// </summary>
    void Start()
    {
        StartTutorial();
    }

    /// <summary>
    /// starts the tutorial
    /// </summary>
    public void StartTutorial()
    {
        _currentPlayer = Instantiate(_playerPrefab, Vector3.zero, Quaternion.identity);
        _currentPlayerControl = _currentPlayer.GetComponent<PlayerControl>();

        _currentPlayerControl.TutorialControls();
        _dialogueTrigger.TriggerDialogue();
    }

    /// <summary>
    /// calls to unlock acceleration for player
    /// </summary>
    public void AccelerationTutorial()
    {
        _accelerationObjective.SetActive(true);
        _currentPlayerControl.UnlockTutorialAcceleration();
    }

    /// <summary>
    /// calls to unlock Strafing for player
    /// </summary>
    public void StrafingTutorial()
    {
        _strafeObjective.SetActive(true);
        _currentPlayerControl.UnlockTutorialStrafing();
    }

    /// <summary>
    /// calls to unlock Black Hole for player
    /// </summary>
    public void BlackHoleTutorial()
    {
        _blackholeObjective.SetActive(true);
        _currentPlayerControl.UnlockTutorialBlackhole();
    }

    /// <summary>
    /// calls to unlock Shooting for player
    /// </summary>
    public void ShootingTutorial()
    {
        _currentPlayerControl.UnlockTutorialShooting();
    }
}
