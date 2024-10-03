using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [10/03/2024]
 * [script to set model]
 */

public class EnemyAsteroidModel : MonoBehaviour
{
    private EnemyAsteroidEventBus _enemyAsteroidEventBus;

    [SerializeField] private GameObject _bigModel;
    [SerializeField] private GameObject _mediumModel;

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
        _bigModel.SetActive(false);
        _mediumModel.SetActive(false);
        _enemyAsteroidEventBus.Unsubscribe(EnemyAsteroidSizeEnum.BIG, SetBig);
        _enemyAsteroidEventBus.Unsubscribe(EnemyAsteroidSizeEnum.MEDIUM, SetMedium);
    }

    /// <summary>
    /// sets model to big asteroid
    /// </summary>
    private void SetBig()
    {
        _bigModel.SetActive(true);
        if (PlaytestData.Instance != null)
        {
            PlaytestData.Instance.bigAsteroidSpawn++;
        }
    }

    /// <summary>
    /// sets model to med asteroid
    /// </summary>
    private void SetMedium()
    {
        _mediumModel.SetActive(true);
        if (PlaytestData.Instance != null)
        {
            PlaytestData.Instance.mediumAsteroidSpawn++;
        }
    }
}
