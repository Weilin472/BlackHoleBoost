using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIManager : MonoBehaviour
{
    [SerializeField] TMP_Text _lifeText;
    [SerializeField] TMP_Text _timerText;
    private static UIManager _instance;
    public static UIManager Instance => _instance;

    private float _currentTime;

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
    }
   
    public void SetLifeUI(int life)
    {
        _lifeText.text = "Life: " + life;
    }

    private void Update()
    {
        if (GameManager.Instance.GamePlaying)
        {
            _currentTime += Time.deltaTime;
            _timerText.text =System.Math.Round(_currentTime, 2).ToString();
         //   _timerText.text = ((int)_currentTime).ToString();
        }
    }

   
}
