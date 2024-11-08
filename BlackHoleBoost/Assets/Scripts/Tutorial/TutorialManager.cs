using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [11/06/2024]
 * [Manages the tutorial]
 */

public class TutorialManager : Singleton<TutorialManager>
{
    [SerializeField] private DialogueTrigger _dialogueTrigger;
    [SerializeField] private GameObject _playerPrefab;
    private GameObject _currentPlayer;
    private PlayerControl _currentPlayerControl;
    private bool _inTutorial = false;

    private PickupSmallAsteroidPool _pickupSmallAsteroidPool;
    private ShootSmallAsteroidPool _shootSmallAsteroidPool;
    private EnemyAsteroidPool _enemyAsteroidPool;
    [SerializeField] private GameObject _dialogueBox;
    [SerializeField] private GameObject _objectiveBox;

    [SerializeField] private GameObject _accelerationObjective;

    [SerializeField] private GameObject _blackholeObjective;

    [SerializeField] private GameObject _strafeObjective;

    [SerializeField] private GameObject _shootObjective;

    private void OnEnable()
    {
        _enemyAsteroidPool = GameManager.Instance.gameObject.GetComponent<EnemyAsteroidPool>();
        _pickupSmallAsteroidPool = GameManager.Instance.gameObject.GetComponent<PickupSmallAsteroidPool>();
        _shootSmallAsteroidPool = GameManager.Instance.gameObject.GetComponent<ShootSmallAsteroidPool>();
    }

    /// <summary>
    /// starts tutorial (temp)
    /// </summary>
    void Start()
    {
        StartTutorial();

    }


    /// <summary>
    /// respawns player if they die
    /// </summary>
    private void Update()
    {
        if (_inTutorial && _currentPlayer == null)
        {
            SpawnPlayer();
        }
    }

    /// <summary>
    /// starts the tutorial
    /// </summary>
    public void StartTutorial()
    {
        SpawnPlayer();

        _inTutorial = true;

        _currentPlayerControl.TutorialControls();
        _dialogueTrigger.TriggerDialogue();
    }

    private void SpawnPlayer()
    {
        _currentPlayer = Instantiate(_playerPrefab, Vector3.zero, Quaternion.identity);
        _currentPlayerControl = _currentPlayer.GetComponent<PlayerControl>();
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
    }

    /// <summary>
    /// calls to unlock Black Hole for player
    /// </summary>
    public void BlackHoleTutorial()
    {
        _blackholeObjective.SetActive(true);
        _currentPlayerControl.UnlockTutorialBlackhole();
        _currentPlayerControl.UnlockTutorialStrafing();
    }

    /// <summary>
    /// calls to unlock Shooting for player
    /// </summary>
    public void ShootingTutorial()
    {
        _shootObjective.SetActive(true);
        _currentPlayerControl.UnlockTutorialShooting();
    }

    /// <summary>
    /// ends tutorial when needed
    /// </summary>
    public void EndTutorial()
    {
        _inTutorial = false;
        _currentPlayerControl.EndTutorial();
        _shootSmallAsteroidPool.ReturnAllShootAsteroids();
        _enemyAsteroidPool.ReturnAllEnemyAsteroids();
        _pickupSmallAsteroidPool.ReturnAllPickupAsteroids();

        _dialogueBox.SetActive(false);
        _objectiveBox.SetActive(false);

        Destroy(_currentPlayer);

        StateMachine.Instance.ChangeState(new MainMenuState());
        ///go back to main menu
    }
}
