using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class StateMachine : Singleton<StateMachine>
{
    private GameState _currentState;

    private void Update()
    {
        _currentState.StateInProgress();
    }

    public void ChangeState(GameState state)
    {
        _currentState = state;
        _currentState.StateStart();
    }
}
