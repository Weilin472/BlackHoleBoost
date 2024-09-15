using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

/*
 * Author: [Lam, Justin]
 * Last Updated: [09/11/2024]
 * [script for pickupable small asteroids, make sure on bottom of component list]
 */

public class PickupSmallAsteroid : MonoBehaviour
{
    private PickupSmallAsteroidEventBus _pickupSmallAsteroidEventBus;

    public IObjectPool<PickupSmallAsteroid> Pool { get; set; }

    /// <summary>
    /// gets needed components
    /// </summary>
    private void Awake()
    {
        _pickupSmallAsteroidEventBus = GetComponent<PickupSmallAsteroidEventBus>();
    }

    private void OnEnable()
    {
        float size = Random.Range(0f, 1f);
        if (size <= .33)
        {
            gameObject.name = "Pickup Normal Asteroid";
            _pickupSmallAsteroidEventBus.Publish(SmallAsteroidType.NORMAL);
        }
        else if (size > .33 && size <= .66)
        {
            gameObject.name = "Pickup Bounce Asteroid";
            _pickupSmallAsteroidEventBus.Publish(SmallAsteroidType.BOUNCE);
        }
        else
        {
            gameObject.name = "Pickup Sticky Asteroid";
            _pickupSmallAsteroidEventBus.Publish(SmallAsteroidType.STICKY);
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
