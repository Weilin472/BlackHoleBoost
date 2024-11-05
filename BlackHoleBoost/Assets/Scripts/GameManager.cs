using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    public float RightBoundary;
    public float TopBoundary;

    public bool GamePlaying;

    //temp bool so prototype can work
    [SerializeField] public bool _inPrototype = true;

    [SerializeField] private GameObject _firstShipPrefab;
    [SerializeField] private GameObject _secondShipPrefab;

    public List<PlayerControl> players=new List<PlayerControl>();

    private void Awake()
    {
        
        if (_instance!=null )
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }

        RightBoundary = Camera.main.orthographicSize * Screen.width / Screen.height;
        TopBoundary = Camera.main.orthographicSize;

        if (_inPrototype)
        {
            SpawnShip();
        }

    }

    private void Start()
    {
        GamePlaying = true;
    }

    private void SpawnShip()
    {
        PlayerInput p = null;
        InputDevice[] devices = null;

        if (Gamepad.all.Count >= 1)
        {
            devices = new InputDevice[] { Keyboard.current, Gamepad.all[0] };
        }
        else
        {
            devices = new InputDevice[] { Keyboard.current };
        }
        p = PlayerInput.Instantiate(_firstShipPrefab, pairWithDevices: devices);
        p.gameObject.transform.position = Vector3.zero + Vector3.left*2;
        players.Add(p.transform.GetComponent<PlayerControl>());
        //p.transform.GetComponent<MeshRenderer>().material.color = LookOfPlayerShip.FirstPlayerBodyColor;
        //p.transform.Find("Head").GetComponent<MeshRenderer>().material.color = LookOfPlayerShip.FirstPlayerHeadColor;

        if (Gamepad.all.Count >= 2)
        {
            devices = new InputDevice[] { Keyboard.current, Gamepad.all[1] };
        }
        else
        {
            devices = new InputDevice[] { Keyboard.current };
        }
        p = PlayerInput.Instantiate(_secondShipPrefab, pairWithDevices: devices);
        p.gameObject.transform.position = Vector3.zero + Vector3.right * 2;
        players.Add(p.transform.GetComponent<PlayerControl>());
        p.transform.GetComponent<MeshRenderer>().material.color = LookOfPlayerShip.SecondPlayerBodyColor;
        p.transform.Find("Head").GetComponent<MeshRenderer>().material.color = LookOfPlayerShip.SecondPlayerHeadColor;

    }
    
    public PlayerControl GetPlayerWithMoreHealth()
    {
        if (players.Count==1)
        {
            return players[0];
        }
        else if (players.Count>1)
        {
            return players[0].transform.GetComponent<PlayerHealthScript>().CurrentHealth > players[1].transform.GetComponent<PlayerHealthScript>().CurrentHealth ? players[0] : players[1];
        }
        else
        {
            return null;
        }
    }


}
