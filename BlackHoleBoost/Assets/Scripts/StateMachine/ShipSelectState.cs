using UnityEngine.SceneManagement;

public class ShipSelectState : GameState
{
    public override void StateInProgress()
    {
       
    }

    public override void StateStart()
    {
        SceneManager.LoadScene("ShipSelectScreen");
    }
}
