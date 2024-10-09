using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [10/07/2024]
 * [inventory for player]
 */

public class PlayerAsteroidInventory : MonoBehaviour
{
    private List<SmallAsteroidType> _inventory;

    [SerializeField] private int _startingNormal = 3;
    [SerializeField] private int _startingBounce = 0;
    [SerializeField] private int _startingSticky = 0;

    [SerializeField] private GameObject[] _displaySlot;
    private MeshRenderer[] _displaySlotRenderer;

    [SerializeField] private Material _displayNormal;
    [SerializeField] private Material _displayBounce;
    [SerializeField] private Material _displaySticky;

    /// <summary>
    /// gets needed components
    /// </summary>
    private void Awake()
    {
        _inventory = new List<SmallAsteroidType>();
        _displaySlotRenderer = new MeshRenderer[5];

        for (int i = 0; i < _displaySlot.Length; i++)
        {
            _displaySlotRenderer[i] = _displaySlot[i].GetComponent<MeshRenderer>();
            _displaySlot[i].SetActive(false);
        }

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
            AddAsteroid(SmallAsteroidType.STICKY);
        }
    }

    private void Update()
    {
        for (int i = 0; i < _displaySlot.Length; i++)
        {
            if (_inventory.Count > i)
            {
                _displaySlot[i].SetActive(true);

                switch (_inventory[i])
                {
                    case SmallAsteroidType.NORMAL:
                        _displaySlotRenderer[i].material = _displayNormal;
                        break;
                    case SmallAsteroidType.BOUNCE:
                        _displaySlotRenderer[i].material = _displayBounce;
                        break;
                    case SmallAsteroidType.STICKY:
                        _displaySlotRenderer[i].material = _displaySticky;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                _displaySlot[i].SetActive(false);
            }
        }

    }

    /// <summary>
    /// adds an asteroid to the inventory
    /// </summary>
    /// <param name="asteroid"></param>
    public void AddAsteroid(SmallAsteroidType asteroid)
    {
        if (_inventory.Count <= 5)
        {
            _inventory.Add(asteroid);

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
    /// gets next asteroid for shoot
    /// takes out asteroid for shoot
    /// only should be used for shoot
    /// </summary>
    /// <returns>asteroid type to shoot</returns>
    public SmallAsteroidType PopNextShootAsteroid()
    {
        if (_inventory.Count > 0)
        {
            SmallAsteroidType smallAsteroidType = _inventory[0];

            _inventory.RemoveAt(0);

            return smallAsteroidType;
        }
        return SmallAsteroidType.NONE;
    }
}
