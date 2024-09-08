using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [09/07/2024]
 * [script for enemy health]
 */

public class EnemyAsteroidHealth : BaseHealthScript
{
    private EnemyAsteroid _enemyAsteroid;

    [SerializeField] private int _bigHealth = 3;
    [SerializeField] private int _smallHealth = 1;

    /// <summary>
    /// on start: get needed components
    /// </summary>
    private void Awake()
    {
        _enemyAsteroid = GetComponent<EnemyAsteroid>();
    }

    /// <summary>
    /// sets health to big asteroid
    /// </summary>
    public void SetBig()
    {
        _maxHealth = _bigHealth;
        ResetHealth();
    }

    /// <summary>
    /// sets health to med asteroid
    /// </summary>
    public void SetMedium()
    {
        _maxHealth = _smallHealth;
        ResetHealth();
    }

    /// <summary>
    /// on death: calls to drop asteroid and return to pool
    /// </summary>
    protected override void OnDeath()
    {
        Debug.Log("IMPLEMENT SMALL ASTEROID DROP");
        _enemyAsteroid.ReturnToPool();
    }
}
