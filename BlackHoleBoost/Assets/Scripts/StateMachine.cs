using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState
{
    Start,
    Playing,
    End,
}
public class StateMachine : MonoBehaviour
{
   

    private GameState _currentState;

    private static StateMachine _instance;
    public static StateMachine Instance => _instance;

    private void Awake()
    {
        if (_instance!=null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void GameStart()
    {
        _currentState = GameState.Start;
        GameManager.Instance.GamePlaying = false;
    }

    public void GamePlaying()
    {
        if (_currentState==GameState.Start)
        {
            _currentState = GameState.Playing;
            GameManager.Instance.GamePlaying = true;
        }
    }

    public void GameEnd()
    {
        if (_currentState==GameState.Playing)
        {
            _currentState = GameState.End;
            GameManager.Instance.GamePlaying = false;
        }
    }
}
