using UnityEngine.SceneManagement;


public class GameStartState : GameState
{
    public override void StateInProgress()
    {

    }

    public override void StateStart()
    {
        SceneManager.LoadScene("Weilin's Scene");

        GameManager.Instance.ResetAsteroids();


        GameManager.Instance.GetComponent<EnemyAsteroidSpawner>().StartSpawning();
    }
}
