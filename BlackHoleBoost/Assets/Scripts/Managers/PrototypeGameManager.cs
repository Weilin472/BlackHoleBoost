using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/*
 * Author: [Lam, Justin]
 * Last Updated: [09/23/2024]
 * [game manager for prototype (please dont use after prototype)]
 */

public class PrototypeGameManager : Singleton<PrototypeGameManager>
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

    private void OnEnable()
    {
        ShowTitleScreen();
    }

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

    public void GameOver()
    {
        _isPlaying = false;
        ShowGameOverUI();

        _enemyAsteroidSpawner.StopSpawning();
        _prototypeEnemySpawner.StopSpawning();
        _enemyAsteroidPool.ReturnAllEnemyAsteroids();
        _shootSmallAsteroidPool.ReturnAllShootAsteroids();
        _pickupSmallAsteroidPool.ReturnAllPickupAsteroids();
    }

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

    //add time
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

    public void ToggleControls()
    {
        _controlText.enabled = !_controlText.enabled;
        _controlIsToggled = !_controlIsToggled;
    }

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
