using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [09/11/2024]
 * [inventory for player]
 */

public class PlayerAsteroidInventory : MonoBehaviour
{
    [SerializeField] private List<SmallAsteroidType> _inventory;

    private void Awake()
    {
        _inventory = new List<SmallAsteroidType>();
    }

    public void AddAsteroid(SmallAsteroidType asteroid)
    {
        if (_inventory.Count <= 5)
        {
            _inventory.Add(asteroid);
        }
    }
}
