using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [11/14/2024]
 * [inventory for player]
 */

public class PlayerAsteroidInventory : MonoBehaviour
{
    private PlayerShoot _playerShoot;

    private int _normalInventory = 0;
    private int _bounceInventory = 0;
    private int _stickyInventory = 0;

    [SerializeField] private int _maxSize = 5;

    [SerializeField] private int _startingNormal = 3;
    [SerializeField] private int _startingBounce = 0;
    [SerializeField] private int _startingSticky = 0;

    /// <summary>
    /// gets needed components
    /// </summary>
    private void Awake()
    {
        _playerShoot = GetComponent<PlayerShoot>();

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

    /// <summary>
    /// updates the displaye
    /// </summary>
    private void Update()
    {
        UpdateDisplay();
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

    /// <summary>
    /// checks if player has a specific asteroid in inventory 
    /// </summary>
    /// <param name="asteroid">asteroid type</param>
    /// <returns>true if the player has at least one of the type</returns>
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
    /// calls to update the display
    /// </summary>
    private void UpdateDisplay()
    {
        SmallAsteroidType nextAsteroid = NextAsteroid(_playerShoot.currentDisplayAsteroidType);
        SmallAsteroidType lastAsteroid = NextAsteroid(nextAsteroid);


        UIManager.Instance.UpdateDisplayInventroy(_playerShoot.currentDisplayAsteroidType, nextAsteroid, lastAsteroid, 
            TypeAmount(_playerShoot.currentDisplayAsteroidType), TypeAmount(nextAsteroid), TypeAmount(lastAsteroid));
    }

    /// <summary>
    /// gets the next asteroid type in order
    /// </summary>
    /// <param name="asteroid">asteroid type being checked</param>
    /// <returns>the next asteroid type</returns>
    private SmallAsteroidType NextAsteroid(SmallAsteroidType asteroid)
    {
        int nextAsteroid = (int)asteroid + 1;
        if (nextAsteroid > 3)
        {
            nextAsteroid = 1;
        }
        return (SmallAsteroidType)nextAsteroid;
    }

    /// <summary>
    /// returns the amount of asteroid the player has of that type
    /// </summary>
    /// <param name="asteroidType">type of asteroid being checked</param>
    /// <returns>number of asteroids</returns>
    private int TypeAmount(SmallAsteroidType asteroidType)
    {
        switch (asteroidType)
        {
            case SmallAsteroidType.NORMAL:
                return _normalInventory;
            case SmallAsteroidType.BOUNCE:
                return _bounceInventory;
            case SmallAsteroidType.STICKY:
                return _stickyInventory;
            default:
                break;
        }
        return -1;
    }

    //properties
    public int normalInventory
    {
        get { return _normalInventory; }
    }

    public int bounceInventory
    {
        get { return _bounceInventory; }
    }

    public int stickyInventory
    {
        get { return _stickyInventory; }
    }
}
