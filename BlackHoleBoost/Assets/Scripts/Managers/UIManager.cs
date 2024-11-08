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
