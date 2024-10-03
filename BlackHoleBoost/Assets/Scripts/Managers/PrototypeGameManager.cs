using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/*
 * Author: [Lam, Justin]
 * Last Updated: [10/03/2024]
 * [game manager for prototype (please dont use after prototype)]
 */

public class PrototypeGameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _livesText;
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private TMP_Text _controlText;
    [SerializeField] private TMP_Text _rulesText;
    [SerializeField] private TMP_Text _titleText;

    [SerializeField] private GameObject _playButton;
    [SerializeField] private GameObject _quitButton;
    [SerializeField] private GameObject _toggleControlsButton;
    [SerializeField] private GameObject _toggleRulesButton;

    private bool _controlIsToggled = true;
    private bool _ruleIsToggled = true;

    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private EnemyAsteroidSpawner _enemyAsteroidSpawner;
    [SerializeField] private EnemyAsteroidPool _enemyAsteroidPool;
    [SerializeField] private ShootSmallAsteroidPool _shootSmallAsteroidPool;
    [SerializeField] private PickupSmallAsteroidPool _pickupSmallAsteroidPool;
    [SerializeField] private PrototypeEnemySpawner _prototypeEnemySpawner;
    private bool _isPlaying = false;
    private int _time;

    /// <summary>
    /// starts game off in title screen
    /// </summary>
    private void OnEnable()
    {
        ShowTitleScreen();
        Debug.LogError("needed this to be not static, change MonoBehavior to Singleton<PrototypeGameManager>");

    }

    /// <summary>
    /// hides all ui
    /// </summary>
    public void HideAllUI()
    {
        _livesText.enabled = false;
        _timerText.enabled = false;
        _controlText.enabled = false;
        _rulesText.enabled = false;
        _titleText.enabled = false;
        _playButton.SetActive(false);
        _quitButton.SetActive(false);
        _toggleControlsButton.SetActive(false);
        _toggleRulesButton.SetActive(false);
    }

    /// <summary>
    /// starts game
    /// </summary>
    public void PlayGame()
    {
        ShowGameUI();
        _isPlaying = true;
        _time = 0;
        _timerText.text = "0:00";
        StartCoroutine(Timer());

        _enemyAsteroidSpawner.StartSpawning();
        _prototypeEnemySpawner.StartSpawning();

        Instantiate(_playerPrefab, Vector3.zero, Quaternion.identity);
    }

    /// <summary>
    /// stops game
    /// </summary>
    public void GameOver()
    {
        _isPlaying = false;
        ShowGameOverUI();

        //feedback tools code
        if (PlaytestData.Instance != null)
        {
            PlaytestData.Instance.secondsSurvived = _time;
        }

        _enemyAsteroidSpawner.StopSpawning();
        _prototypeEnemySpawner.StopSpawning();
        _enemyAsteroidPool.ReturnAllEnemyAsteroids();
        _shootSmallAsteroidPool.ReturnAllShootAsteroids();
        _pickupSmallAsteroidPool.ReturnAllPickupAsteroids();
    }

    /// <summary>
    /// shows ui for title screen
    /// </summary>
    public void ShowTitleScreen()
    {
        HideAllUI();

        _titleText.enabled = true;
        _titleText.text = "Black Hole Boost Core Loop Prototype";

        if (_controlIsToggled)
        {
            _controlText.enabled = true;
        }
        if (_ruleIsToggled)
        {
            _rulesText.enabled = true;
        }

        _playButton.SetActive(true);
        _quitButton.SetActive(true);
        _toggleControlsButton.SetActive(true);
        _toggleRulesButton.SetActive(true);
    }

    /// <summary>
    /// shows ui for game
    /// </summary>
    public void ShowGameUI()
    {
        HideAllUI();

        _livesText.enabled = true;
        _timerText.enabled = true;

        if (_controlIsToggled)
        {
            _controlText.enabled = true;
        }
        if (_ruleIsToggled)
        {
            _rulesText.enabled = true;
        }
    }

    /// <summary>
    /// shows UI for game over
    /// </summary>
    public void ShowGameOverUI()
    {
        HideAllUI();

        _titleText.enabled = true;
        _titleText.text = "Game Over\nYou survived: " + _timerText.text;

        if (_controlIsToggled)
        {
            _controlText.enabled = true;
        }
        if (_ruleIsToggled)
        {
            _rulesText.enabled = true;
        }

        _playButton.SetActive(true);
        _quitButton.SetActive(true);
        _toggleControlsButton.SetActive(true);
        _toggleRulesButton.SetActive(true);
    }

    /// <summary>
    /// toggles controls ui
    /// </summary>
    public void ToggleControls()
    {
        _controlText.enabled = !_controlText.enabled;
        _controlIsToggled = !_controlIsToggled;
    }

    /// <summary>
    /// toggles rules ui
    /// </summary>
    public void ToggleRules()
    {
        _rulesText.enabled = !_rulesText.enabled;
        _ruleIsToggled = !_ruleIsToggled;
    }
    
    /// <summary>
    /// Quits the application
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }

    /// <summary>
    /// timer for the UI
    /// </summary>
    /// <returns></returns>
    private IEnumerator Timer()
    {
        while (_isPlaying)
        {
            yield return new WaitForSeconds(1);
            _time++;
            if (_time % 60 < 10)
            {
                _timerText.text = (_time / 60) + ":0" + (_time % 60);
            }
            else
            {
                _timerText.text = (_time / 60) + ":" + (_time % 60);
            }
        }
    }
}
