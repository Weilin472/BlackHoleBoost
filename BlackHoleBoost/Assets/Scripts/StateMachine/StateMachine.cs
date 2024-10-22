using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class StateMachine : Singleton<StateMachine>
{
    private GameState _currentState;

    private void Update()
    {
        if (_currentState!=null)
        {
            _currentState.StateInProgress();
        }
    }

    public void ChangeState(GameState state)
    {
        if (_currentState!=null)
        {
            _currentState.StateEnd();
        }
        _currentState = state;
        _currentState.StateStart();
    }
}
