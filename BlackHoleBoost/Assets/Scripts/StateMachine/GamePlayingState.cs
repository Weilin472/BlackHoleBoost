using UnityEngine.SceneManagement;

public class GamePlayingState : GameState
{
    public override void StateInProgress()
    {
       
    }

    public override void StateStart()
    {
        SceneManager.LoadScene(1);        
    }
}
