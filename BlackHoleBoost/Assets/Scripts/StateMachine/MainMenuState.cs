using UnityEngine.SceneManagement;

public class MainMenuState : GameState
{
    public override void StateInProgress()
    {

    }

    public override void StateStart()
    {
        SceneManager.LoadScene("TestMainMenuScene");
       

    }
}
