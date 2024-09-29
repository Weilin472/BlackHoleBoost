using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

/*
 * Author: [Lam, Justin]
 * Last Updated: [09/28/2024]
 * [script for pickupable small asteroids, make sure on bottom of component list]
 */

public class ShootSmallAsteroid : MonoBehaviour
{
    private ShootSmallAsteroidEventBus _shootSmallAsteroidEventBus;

    public IObjectPool<ShootSmallAsteroid> Pool { get; set; }

    /// <summary>
    /// gets needed components
    /// </summary>
    private void Awake()
    {
        _shootSmallAsteroidEventBus = GetComponent<ShootSmallAsteroidEventBus>();
    }

    public void SetAsteroid(SmallAsteroidType asteroidType)
    {
        if (asteroidType == SmallAsteroidType.NONE)
        {
            Debug.Log("cant shoot none");
            return;
        }
        else
        {
            gameObject.name = "Shoot "+ asteroidType + " Asteroid";
            _shootSmallAsteroidEventBus.Publish(asteroidType);
        }
    }

    /// <summary>
    /// returns object to pool
    /// </summary>
    public void ReturnToPool()
    {
        Pool.Release(this);
    }
}
