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
        /*
        if (GameManager.Instance.GamePlaying)
        {
            _currentTime += Time.deltaTime;
            _timerText.text =System.Math.Round(_currentTime, 2).ToString();
         //   _timerText.text = ((int)_currentTime).ToString();
        }*/
    }

    public void ShowGameOverPanel()
    {
        gameoverPanel.gameObject.SetActive(true);
        Time.timeScale = 0;
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
