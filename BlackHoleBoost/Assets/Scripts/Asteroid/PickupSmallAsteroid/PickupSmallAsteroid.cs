using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

/*
 * Author: [Lam, Justin]
 * Last Updated: [10/14/2024]
 * [script for pickupable small asteroids, make sure on bottom of component list]
 */

public class PickupSmallAsteroid : MonoBehaviour
{
    private PickupSmallAsteroidEventBus _pickupSmallAsteroidEventBus;

    public IObjectPool<PickupSmallAsteroid> Pool { get; set; }

    [SerializeField] [Range(0f, 1f)] private float _chanceForNormal = .33f;
    [SerializeField] [Range(0f, 1f)] private float _chanceForBounce = .33f;

    /// <summary>
    /// gets needed components
    /// </summary>
    private void Awake()
    {
        _pickupSmallAsteroidEventBus = GetComponent<PickupSmallAsteroidEventBus>();
    }

    /// <summary>
    /// sets asteroid if there isn't a specific type of asteroid
    /// </summary>
    public void SetAsteroid()
    {
        float size = Random.Range(0f, 1f);
        if (size <= _chanceForNormal)
        {
            gameObject.name = "Pickup Normal Asteroid";
            _pickupSmallAsteroidEventBus.Publish(SmallAsteroidType.NORMAL);
        }
        else if (size > _chanceForNormal && size <= _chanceForBounce + _chanceForNormal)
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
    /// sets asteroid to type
    /// </summary>
    /// <param name="type">the type of asteroid being converted to</param>
    public void SetAsteroid(SmallAsteroidType type)
    {
        _pickupSmallAsteroidEventBus.Publish(type);
    }

    /// <summary>
    /// returns object to pool
    /// </summary>
    public void ReturnToPool()
    {
        Pool.Release(this);
    }
}
