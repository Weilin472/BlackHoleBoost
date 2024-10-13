using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*
 * Author: [Lam, Justin]
 * Last Updated: [10/12/2024]
 * [inventory for player]
 */

public class PlayerAsteroidInventory : MonoBehaviour
{
    private int _normalInventory = 0;
    private int _bounceInventory = 0;
    private int _stickyInventory = 0;

    [SerializeField] private int _maxSize = 5;

    [SerializeField] private int _startingNormal = 3;
    [SerializeField] private int _startingBounce = 0;
    [SerializeField] private int _startingSticky = 0;

    [SerializeField] private TMP_Text _normalDisplayText;
    [SerializeField] private TMP_Text _bounceDisplayText;
    [SerializeField] private TMP_Text _stickyDisplayText;

    /// <summary>
    /// gets needed components
    /// </summary>
    private void Awake()
    {
        //starting inventroy
        for (int i = 0; i < _startingNormal; i++)
        {
            AddAsteroid(SmallAsteroidType.NORMAL);
        }
        for (int i = 0; i < _startingSticky; i++)
        {
            AddAsteroid(SmallAsteroidType.STICKY);
        }
        for (int i = 0; i < _startingBounce; i++)
        {
            AddAsteroid(SmallAsteroidType.BOUNCE);
        }
    }

    private void Update()
    {
        DisplayInventrory();
    }

    /// <summary>
    /// adds an asteroid to the inventory
    /// </summary>
    /// <param name="asteroid"></param>
    public void AddAsteroid(SmallAsteroidType asteroid)
    {
        if (_normalInventory + _bounceInventory + _stickyInventory < _maxSize)
        {
            switch (asteroid)
            {
                case SmallAsteroidType.NORMAL:
                    _normalInventory++;
                    break;
                case SmallAsteroidType.BOUNCE:
                    _bounceInventory++;
                    break;
                case SmallAsteroidType.STICKY:
                    _stickyInventory++;
                    break;
                default:
                    break;
            }

            //playtest data stuff here
            if (PlaytestDataCollector.Instance != null)
            {
                PlaytestDataCollector.Instance.totalAsteroidsCollected++;
                switch (asteroid)
                {
                    case SmallAsteroidType.NORMAL:
                        PlaytestDataCollector.Instance.normalAsteroidsCollected++;
                        break;
                    case SmallAsteroidType.BOUNCE:
                        PlaytestDataCollector.Instance.bounceAsteroidsCollected++;
                        break;
                    case SmallAsteroidType.STICKY:
                        PlaytestDataCollector.Instance.stickyAsteroidsCollected++;
                        break;
                    default:
                        break;
                }
            }
        }
    }

    /// <summary>
    /// checks if player can shoot asteroid
    /// if true, shoots asteroid
    /// </summary>
    /// <param name="asteroid">asteroid player is shooting</param>
    /// <returns>if the player can shoot</returns>
    public void OnShootAsteroid(SmallAsteroidType asteroid)
    {
        switch (asteroid)
        {
            case SmallAsteroidType.NORMAL:
                if (_normalInventory > 0)
                {
                    _normalInventory--;
                }
                break;
            case SmallAsteroidType.BOUNCE:
                if (_bounceInventory > 0)
                {
                    _bounceInventory--;
                }
                break;
            case SmallAsteroidType.STICKY:
                if (_stickyInventory > 0)
                {
                    _stickyInventory--;
                }
                break;
            default:
                break;
        }
    }

    public bool HasAsteroidType(SmallAsteroidType asteroid)
    {
        switch (asteroid)
        {
            case SmallAsteroidType.NORMAL:
                if (_normalInventory > 0)
                {
                    return true;
                }
                break;
            case SmallAsteroidType.BOUNCE:
                if (_bounceInventory > 0)
                {
                    return true;
                }
                break;
            case SmallAsteroidType.STICKY:
                if (_stickyInventory > 0)
                {
                    return true;
                }
                break;
            default:
                break;
        }
        return false;
    }

    /// <summary>
    /// displays how much each asteroids the player has
    /// </summary>
    public void DisplayInventrory()
    {
        _normalDisplayText.text = _normalInventory.ToString();
        _bounceDisplayText.text = _bounceInventory.ToString();
        _stickyDisplayText.text = _stickyInventory.ToString();
    }
}
