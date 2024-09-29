using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [09/16/2024]
 * [health script for shoot asteroid]
 */

public class ShootSmallAsteroidHealth : BaseHealthScript
{
    private ShootSmallAsteroid _shootSmallAsteroid;
    private ShootSmallAsteroidEventBus _shootSmallAsteroidEventBus;

    [SerializeField] private int _numOfBounces = 5;

    /// <summary>
    /// gets needed components
    /// </summary>
    private void Awake()
    {
        _shootSmallAsteroidEventBus = GetComponent<ShootSmallAsteroidEventBus>();
        _shootSmallAsteroid = GetComponent<ShootSmallAsteroid>();
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

    protected override void OnDeath()
    {
        _shootSmallAsteroid.ReturnToPool();
    }

    /// <summary>
    /// sets health to normal asteroid
    /// </summary>
    private void SetNormal()
    {
        _maxHealth = 1;
        _invincible = false;
        ResetHealth();
    }

    /// <summary>
    /// sets health to bounce asteroid
    /// </summary>
    private void SetBounce()
    {
        _maxHealth = _numOfBounces;
        _invincible = false;
        ResetHealth();
    }

    /// <summary>
    /// sets health to sticky asteroid
    /// </summary>
    private void SetSticky()
    {
        _maxHealth = 1;
        _invincible = true;
        ResetHealth();
    }
}
