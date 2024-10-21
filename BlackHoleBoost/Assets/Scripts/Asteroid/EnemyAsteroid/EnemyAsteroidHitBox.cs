using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [10/21/2024]
 * [script for the hitbox]
 */

public class EnemyAsteroidHitBox : MonoBehaviour
{
    private EnemyAsteroidEventBus _enemyAsteroidEventBus;

    [SerializeField] private CapsuleCollider _collider;
    [SerializeField] private float _bigHitboxRadius = 0.62f;
    [SerializeField] private float _mediumHitboxRadius = 0.37f;

    /// <summary>
    /// gets needed component
    /// </summary>
    private void Awake()
    {
        _enemyAsteroidEventBus = GetComponent<EnemyAsteroidEventBus>();
    }

    /// <summary>
    /// subscribes to event bus
    /// </summary>
    private void OnEnable()
    {
        _enemyAsteroidEventBus.Subscribe(EnemyAsteroidSizeEnum.BIG, SetBig);
        _enemyAsteroidEventBus.Subscribe(EnemyAsteroidSizeEnum.MEDIUM, SetMedium);
    }

    /// <summary>
    /// disables models and unsubscribes from event bus
    /// </summary>
    private void OnDisable()
    {
        _enemyAsteroidEventBus.Unsubscribe(EnemyAsteroidSizeEnum.BIG, SetBig);
        _enemyAsteroidEventBus.Unsubscribe(EnemyAsteroidSizeEnum.MEDIUM, SetMedium);
    }

    /// <summary>
    /// sets hitbox to big asteroid
    /// </summary>
    private void SetBig()
    {
        _collider.radius = _bigHitboxRadius;
    }

    /// <summary>
    /// sets hitbox to med asteroid
    /// </summary>
    private void SetMedium()
    {
        _collider.radius = _mediumHitboxRadius;
    }
}
