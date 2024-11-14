using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{
    [SerializeField] TMP_Text _lifeText;
    [SerializeField] TMP_Text _timerText;
    [SerializeField] GameObject gameoverPanel;
    [SerializeField] TMP_Text _surviveTimer;

    [SerializeField] Image _currentAsteroidSprite;
    [SerializeField] Image _nextAsteroidSprite;
    [SerializeField] Image _lastAsteroidSprite;

    [SerializeField] TMP_Text _currentAsteroidAmount;
    [SerializeField] TMP_Text _nextAsteroidAmount;
    [SerializeField] TMP_Text _lastAsteroidAmount;

    private static UIManager _instance;
    public static UIManager Instance => _instance;

    private float _currentTime;

    private void Awake()
    {
        if (_instance!=null )
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }
   
    public void SetLifeUI(int life)
    {
        _lifeText.text = "Life: " + life;
    }

    /// <summary>
    /// updates the inventory display
    /// </summary>
    /// <param name="current">type of asteroid for current asteroid</param>
    /// <param name="next">type of asteroid for next asteroid</param>
    /// <param name="last">type of asteroid for last asteroid</param>
    /// <param name="currentAmount">number of current asteroid</param>
    /// <param name="nextAmount">number of next asteroid</param>
    /// <param name="lastAmount">number of last asteroid</param>
    public void UpdateDisplayInventroy(SmallAsteroidType current, SmallAsteroidType next, SmallAsteroidType last, int currentAmount, int nextAmount, int lastAmount)
    {
        SetAsteroidSpriteColor(_currentAsteroidSprite, current);
        SetAsteroidSpriteColor(_nextAsteroidSprite, next);
        SetAsteroidSpriteColor(_lastAsteroidSprite, last);

        _currentAsteroidAmount.text = "" + currentAmount;
        _nextAsteroidAmount.text = "" + nextAmount;
        _lastAsteroidAmount.text = "" + lastAmount;
    }

    /// <summary>
    /// sets the color of the asteroid sprite
    /// </summary>
    /// <param name="asteroidSprite">asteroid image of ui</param>
    /// <param name="asteroidType">type of asteroid</param>
    public void SetAsteroidSpriteColor(Image asteroidSprite, SmallAsteroidType asteroidType)
    {
        switch (asteroidType)
        {
            case SmallAsteroidType.NORMAL:
                asteroidSprite.color = Color.red;
                break;
            case SmallAsteroidType.BOUNCE:
                asteroidSprite.color = Color.blue;
                break;
            case SmallAsteroidType.STICKY:
                asteroidSprite.color = Color.green;
                break;
            default:
                asteroidSprite.color = Color.white;
                break;
        }
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;
        int t = Mathf.RoundToInt(_currentTime);
        if (t % 60 < 10)
        {
            _timerText.text = (t / 60) + ":0" + (t % 60);
        }
        else
        {
            _timerText.text = (t / 60) + ":" + (t % 60);
        }
    }

    public void ShowGameOverPanel()
    {
        if (gameoverPanel != null && _surviveTimer != null)
        {
            gameoverPanel.gameObject.SetActive(true);
            Time.timeScale = 0;
            int t = Mathf.RoundToInt(_currentTime);
            if (t % 60 < 10)
            {
                _surviveTimer.text = "You have survived: " + (t / 60) + ":0" + (t % 60);
            }
            else
            {
                _surviveTimer.text = "You have survived: " + (t / 60) + ": " + (t % 60);
            }
        }
    }

    public void ClickReTryBtn()
    {
        StateMachine.Instance.ChangeState(new GameStartState());
    }

   

    public void ClickQuitBtn()
    {
        Application.Quit();
    }
   
}
