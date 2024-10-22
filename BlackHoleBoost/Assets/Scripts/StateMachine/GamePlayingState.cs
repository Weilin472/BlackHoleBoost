using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayingState : GameState
{
    public void StateInProgress()
    {
       
    }

    public void StateStart()
    {
        GameManager.Instance.GamePlaying = true;
    }
}
