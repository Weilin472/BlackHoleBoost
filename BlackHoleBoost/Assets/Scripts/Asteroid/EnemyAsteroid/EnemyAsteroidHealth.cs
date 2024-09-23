using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [09/22/2024]
 * [script for enemy health]
 */

public class EnemyAsteroidHealth : BaseHealthScript
{
    private EnemyAsteroid _enemyAsteroid;
    private EnemyAsteroidEventBus _enemyAsteroidEventBus;
    private PickupSmallAsteroidPool _pickupSmallAsteroidPool;

    [SerializeField] private int _bigHealth = 3;
    [SerializeField] private int _smallHealth = 1;

    /// <summary>
    /// on start: get needed components
    /// </summary>
    private void Awake()
    {
        _enemyAsteroid = GetComponent<EnemyAsteroid>();
        _enemyAsteroidEventBus = GetComponent<EnemyAsteroidEventBus>();
        _pickupSmallAsteroidPool = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PickupSmallAsteroidPool>();
    }

    private void OnEnable()
    {
        _enemyAsteroidEventBus.Subscribe(EnemyAsteroidSizeEnum.BIG, SetBig);
        _enemyAsteroidEventBus.Subscribe(EnemyAsteroidSizeEnum.MEDIUM, SetMedium);
    }

    private void OnDisable()
    {
        _enemyAsteroidEventBus.Unsubscribe(EnemyAsteroidSizeEnum.BIG, SetBig);
        _enemyAsteroidEventBus.Unsubscribe(EnemyAsteroidSizeEnum.MEDIUM, SetMedium);
    }

    /// <summary>
    /// sets health to big asteroid
    /// </summary>
    private void SetBig()
    {
        _maxHealth = _bigHealth;
        ResetHealth();
    }

    /// <summary>
    /// sets health to med asteroid
    /// </summary>
    private void SetMedium()
    {
        _maxHealth = _smallHealth;
        ResetHealth();
    }

    /// <summary>
    /// on death: calls to drop asteroid and return to pool
    /// </summary>
    protected override void OnDeath()
    {
        //Debug.Log("IMPLEMENT SMALL ASTEROID DROP");
        _pickupSmallAsteroidPool.Spawn(transform.position);
        _enemyAsteroid.ReturnToPool();
    }
}
