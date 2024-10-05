using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [09/08/2024]
 * [sets damage for asteroid]
 */

public class EnemyAsteroidDamage : BaseDamageScript
{
    private EnemyAsteroidEventBus _enemyAsteroidEventBus;

    [SerializeField] private int _bigDamage = 3;
    [SerializeField] private int _mediumDamage = 1;

    /// <summary>
    /// gets needed components
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
    /// unsubscribes from event bus
    /// </summary>
    private void OnDisable()
    {
        _enemyAsteroidEventBus.Unsubscribe(EnemyAsteroidSizeEnum.BIG, SetBig);
        _enemyAsteroidEventBus.Unsubscribe(EnemyAsteroidSizeEnum.MEDIUM, SetMedium);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.gameObject.tag != "Enemy")
        {
            base.OnTriggerEnter(other);
            if (PlaytestDataCollector.Instance != null)
            {
                if (other.transform.root.gameObject.tag == "Player")
                {
                    PlaytestDataCollector.Instance.AddPlayerHit("Asteroid");
                }
            }
        }
    }

    /// <summary>
    /// sets health to big asteroid
    /// </summary>
    private void SetBig()
    {
        _damage = _bigDamage;
    }

    /// <summary>
    /// sets health to med asteroid
    /// </summary>
    private void SetMedium()
    {
        _damage = _mediumDamage;
    }
}
