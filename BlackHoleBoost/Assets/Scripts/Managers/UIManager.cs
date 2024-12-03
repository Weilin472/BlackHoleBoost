using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{
    //[SerializeField] TMP_Text _lifeText;
    [SerializeField] private GameObject[] _lifeBars;

    [SerializeField] TMP_Text _timerText;
    [SerializeField] GameObject gameoverPanel;
    [SerializeField] TMP_Text _surviveTimer;

    [SerializeField] Image _currentAsteroidSprite;
    [SerializeField] Image _nextAsteroidSprite;
    [SerializeField] Image _lastAsteroidSprite;

    [SerializeField] TMP_Text _currentAsteroidAmount;
    [SerializeField] TMP_Text _nextAsteroidAmount;
    [SerializeField] TMP_Text _lastAsteroidAmount;

    [SerializeField] GameObject _inputField;
    [SerializeField] GameObject _leaderBoard;

    [SerializeField] GameObject[] _leaderBoardContents;

    [SerializeField] GameObject _pausePanel;

    [SerializeField] GameObject _settingPanel;

    List<LeaderBoardInfo> infoList;

    private static UIManager _instance;
    public static UIManager Instance => _instance;

    private float _currentTime;

    private bool _inPause = false;

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
        for (int i = 0; i < life; i++)
        {
            _lifeBars[i].SetActive(true);
        }

        if (life < 5)
        {
            for (int i = life; i <= 4; i++)
            {
                _lifeBars[i].SetActive(false);
            }
        }
    }

    /// <summary>
    /// updates the inventory display
    /// </summary>
    /// <param name="current">type of asteroid for current asteroid</param>
    /// <param name="next">type of asteroid for next asteroid</param>
    /// <param name="last">type of asteroid for last asteroid</param>
    /// <param name="currentAmount">number of current asteroid</param>
    /// <param name="nextAmount">number of next asteroid</param>
    /// <param name="lastAmount">number of last asteroid</param>
    public void UpdateDisplayInventroy(SmallAsteroidType current, SmallAsteroidType next, SmallAsteroidType last, int currentAmount, int nextAmount, int lastAmount)
    {
        SetAsteroidSpriteColor(_currentAsteroidSprite, current);
        SetAsteroidSpriteColor(_nextAsteroidSprite, next);
        SetAsteroidSpriteColor(_lastAsteroidSprite, last);

        _currentAsteroidAmount.text = "" + currentAmount;
        _nextAsteroidAmount.text = "" + nextAmount;
        _lastAsteroidAmount.text = "" + lastAmount;
    }

    /// <summary>
    /// sets the color of the asteroid sprite
    /// </summary>
    /// <param name="asteroidSprite">asteroid image of ui</param>
    /// <param name="asteroidType">type of asteroid</param>
    public void SetAsteroidSpriteColor(Image asteroidSprite, SmallAsteroidType asteroidType)
    {
        switch (asteroidType)
        {
            case SmallAsteroidType.NORMAL:
                asteroidSprite.color = Color.red;
                break;
            case SmallAsteroidType.BOUNCE:
                asteroidSprite.color = Color.blue;
                break;
            case SmallAsteroidType.STICKY:
                asteroidSprite.color = Color.green;
                break;
            default:
                asteroidSprite.color = Color.white;
                break;
        }
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;
        int t = Mathf.RoundToInt(_currentTime);
        if (t % 60 < 10)
        {
            _timerText.text = (t / 60) + ":0" + (t % 60);
        }
        else
        {
            _timerText.text = (t / 60) + ":" + (t % 60);
        }
    }

    public void ShowGameOverPanel()
    {
        _inPause = false;
        if (gameoverPanel != null && _surviveTimer != null)
        {
            gameoverPanel.gameObject.SetActive(true);
            Time.timeScale = 0;
            int t = Mathf.RoundToInt(_currentTime);
            if (t % 60 < 10)
            {
                _surviveTimer.text = "You have survived: " + (t / 60) + ":0" + (t % 60);
            }
            else
            {
                _surviveTimer.text = "You have survived: " + (t / 60) + ":" + (t % 60);
            }
        }
        if (PlayerPrefs.HasKey(GameManager.LeaderBoardSavingString))
        {
            string dataString = PlayerPrefs.GetString(GameManager.LeaderBoardSavingString);
            SavingandLoadingData data = JsonUtility.FromJson<SavingandLoadingData>(dataString);
            infoList = data.LeaderBoardDataList;
            Debug.Log("loadagain: "+infoList.Count);
        }
        else
        {
            infoList = new List<LeaderBoardInfo>();
            Debug.Log("numCount: " + GameManager.LeaderBoardPlayerNum);
            for (int i = 0; i < GameManager.LeaderBoardPlayerNum; i++)
            {
                infoList.Add(new LeaderBoardInfo("", 0, 0));
            }
        }
        EnemySpawner spawner = FindObjectOfType<EnemySpawner>();
        int rankIndex = CompareLeaderBoard(infoList, spawner.Phase, (int)_currentTime);
        if (rankIndex!=-1)
        {
            _inputField.gameObject.SetActive(true);
            _leaderBoard.gameObject.SetActive(false);
        }
        else
        {
            _inputField.gameObject.SetActive(false);
            _leaderBoard.gameObject.SetActive(true);
            SetUpLeaderBoard();
        }
    }

    private int CompareLeaderBoard(List<LeaderBoardInfo> infoList,int survivedPhases, int survivedTime)
    {
        for (int i = 0; i < infoList.Count; i++)
        {
            LeaderBoardInfo info = infoList[i];
            if (survivedPhases>info.SurvivedPhases||(survivedPhases==info.SurvivedPhases&&survivedTime>info.SurvivedTime))
            {
                return i;
            }
        }
        return -1;
    }

    private void SetUpLeaderBoard()
    {
        if (infoList!=null)
        {
            for (int i = 0; i < _leaderBoardContents.Length; i++)
            {
                Debug.Log("index: " + i);
                _leaderBoardContents[i].transform.Find("name").GetComponent<TMP_Text>().text = infoList[i].Name;
                _leaderBoardContents[i].transform.Find("Phase").GetComponent<TMP_Text>().text = (infoList[i].SurvivedPhases+1).ToString();
                int time = infoList[i].SurvivedTime;
                string timeString = (time / 60) +":"+ (time % 60 < 10 ? ("0" + time % 60) : (time % 60).ToString());
                _leaderBoardContents[i].transform.Find("Time").GetComponent<TMP_Text>().text = timeString;
            }
        }
    }

    public void ClickReTryBtn()
    {
        if (!GameObject.FindGameObjectWithTag("TutorialManager"))
        {
            StateMachine.Instance.ChangeState(new GameStartState());
        }
    }  

    public void ClickQuitBtn()
    {
        Application.Quit();
    }

    public void EnterName(string s)
    {
        if (s!="")
        {
            if (infoList != null)
            {
                EnemySpawner spawner = FindObjectOfType<EnemySpawner>();
                int rankIndex = CompareLeaderBoard(infoList, spawner.Phase, (int)_currentTime);
                LeaderBoardInfo info = new LeaderBoardInfo(s, spawner.Phase, (int)_currentTime);
                infoList.Insert(rankIndex, info);
                infoList.RemoveAt(infoList.Count - 1);
                SavingandLoadingData data = new SavingandLoadingData();
                data.LeaderBoardDataList = infoList;
                Debug.Log("save: " + infoList.Count);
                string dataString = JsonUtility.ToJson(data);
                PlayerPrefs.SetString(GameManager.LeaderBoardSavingString, dataString);
                PlayerPrefs.Save();
            }
        }
        SetUpLeaderBoard();
        _inputField.gameObject.SetActive(false);
        _leaderBoard.gameObject.SetActive(true);
    }

    public void ShowPauseMenu()
    {
        _pausePanel.gameObject.SetActive(true);
        Time.timeScale = 0;
        _inPause = true;
    }

    public void ContienueBtn()
    {
        _pausePanel.gameObject.SetActive(false);
        Time.timeScale = 1;
        _inPause = false;
    }

    public void ShowSettingMenu()
    {
        StateMachine.Instance.ChangeState(new ActiveMenuState(_settingPanel));
    }

    /// <summary>
    /// property for pause
    /// </summary>
    public bool inPause
    {
        get { return _inPause; }
    }
}
