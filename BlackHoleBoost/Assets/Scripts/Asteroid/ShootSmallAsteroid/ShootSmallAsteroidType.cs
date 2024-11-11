using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [11/10/2024]
 * [tells which type of asteroid this is for cerberus]
 */

public class ShootSmallAsteroidType : MonoBehaviour
{
    private ShootSmallAsteroidEventBus _shootSmallAsteroidEventBus;

    private SmallAsteroidType _asteroidType;

    /// <summary>
    /// gets needed components
    /// </summary>
    private void Awake()
    {
        _shootSmallAsteroidEventBus = GetComponent<ShootSmallAsteroidEventBus>();
    }

    /// <summary>
    /// subscribes to event bus
    /// </summary>
    private void OnEnable()
    {
        _shootSmallAsteroidEventBus.Subscribe(SmallAsteroidType.NORMAL, SetNormal);
        _shootSmallAsteroidEventBus.Subscribe(SmallAsteroidType.BOUNCE, SetBounce);
        _shootSmallAsteroidEventBus.Subscribe(SmallAsteroidType.STICKY, SetSticky);
    }

    /// <summary>
    /// unsubscribes from event bus
    /// </summary>
    private void OnDisable()
    {
        _shootSmallAsteroidEventBus.Unsubscribe(SmallAsteroidType.NORMAL, SetNormal);
        _shootSmallAsteroidEventBus.Unsubscribe(SmallAsteroidType.BOUNCE, SetBounce);
        _shootSmallAsteroidEventBus.Unsubscribe(SmallAsteroidType.STICKY, SetSticky);
    }

    /// <summary>
    /// sets type to normal asteroid
    /// </summary>
    private void SetNormal()
    {
        _asteroidType = SmallAsteroidType.NORMAL;
    }

    /// <summary>
    /// sets type to bounce asteroid
    /// </summary>
    private void SetBounce()
    {
        _asteroidType = SmallAsteroidType.BOUNCE;
    }

    /// <summary>
    /// sets type to sticky asteroid
    /// </summary>
    private void SetSticky()
    {
        _asteroidType = SmallAsteroidType.STICKY;
    }

    public SmallAsteroidType GetAsteroidType()
    {
        return _asteroidType;
    }
}
