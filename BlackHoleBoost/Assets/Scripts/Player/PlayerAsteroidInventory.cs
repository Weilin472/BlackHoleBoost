using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [09/22/2024]
 * [inventory for player]
 */

public class PlayerAsteroidInventory : MonoBehaviour
{
    private List<SmallAsteroidType> _inventory;
    public SmallAsteroidType[] displayInventory;

    /// <summary>
    /// gets needed components
    /// </summary>
    private void Awake()
    {
        _inventory = new List<SmallAsteroidType>();
        displayInventory = new SmallAsteroidType[5];
    }

    /// <summary>
    /// displays inventory in ispector
    /// </summary>
    private void Update()
    {
        for (int i = 0; i < _inventory.Count; i++)
        {
            displayInventory[i] = _inventory[i];
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
        }
    }

    /// <summary>
    /// gets next asteroid for shoot
    /// takes out asteroid for shoot
    /// </summary>
    /// <returns></returns>
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
