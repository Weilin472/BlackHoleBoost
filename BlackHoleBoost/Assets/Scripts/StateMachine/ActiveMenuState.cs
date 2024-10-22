
using UnityEngine;
using UnityEngine.UI;

public class ActiveMenuState : GameState
{
    private GameObject _menu;
    public ActiveMenuState(GameObject menu)
    {
        _menu = menu;
    }
    public override void StateInProgress()
    {
        
    }

    public override void StateStart()
    {
        _menu.gameObject.SetActive(true);
        _menu.transform.Find("ReturnBtn").transform.GetComponent<Button>().onClick.AddListener(() => _menu.gameObject.SetActive(false));
    }

    public override void StateEnd()
    {
        _menu.transform.Find("ReturnBtn").transform.GetComponent<Button>().onClick.RemoveListener(() => _menu.gameObject.SetActive(false));
    }


}
