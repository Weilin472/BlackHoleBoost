using UnityEngine.SceneManagement;

public class TutorialState : GameStartState
{
    public override void StateInProgress()
    {

    }

    public override void StateStart()
    {
        SceneManager.LoadScene("TutorialTest");

    }
}
