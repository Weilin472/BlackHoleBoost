using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverState : GameState
{
    public override void StateInProgress()
    {
        
    }

    public override void StateStart()
    {
        UIManager.Instance.ShowGameOverPanel();
        GameManager.Instance.GetComponent<EnemyAsteroidSpawner>().StopSpawning();
        GameManager.Instance.ResetAsteroids();
    }


}
