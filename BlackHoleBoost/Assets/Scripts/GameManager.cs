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
    }
    


}
