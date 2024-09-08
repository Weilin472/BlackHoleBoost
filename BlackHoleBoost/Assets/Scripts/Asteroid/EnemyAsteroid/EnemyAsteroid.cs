using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

/*
 * Author: [Lam, Justin]
 * Last Updated: [09/08/2024]
 * [Script for enemy asteroids
 * MAKE SURE THIS IS BOTTOM OF SCRIPT COPONENTS SO ON ENABLE WORKS RIGH]
 */

public class EnemyAsteroid : MonoBehaviour
{
    private EnemyAsteroidHealth _enemyAsteroidHealth;
    private EnemyAsteroidModel _enemyAsteroidModel;
    private EnemyAsteroidDamage _enemyAsteroidDamage;

    private EnemyAsteroidEventBus _enemyAsteroidEventBus;

    public IObjectPool<EnemyAsteroid> Pool { get; set; }

    [SerializeField][Range(0, 1)]
    private float _chanceForBig = 0.5f;

    /// <summary>
    /// on Start: get needed scripts
    /// </summary>
    private void Awake()
    {
        _enemyAsteroidHealth = GetComponent<EnemyAsteroidHealth>();
        _enemyAsteroidModel = GetComponent<EnemyAsteroidModel>();
        _enemyAsteroidDamage = GetComponent<EnemyAsteroidDamage>();

        _enemyAsteroidEventBus = GetComponent<EnemyAsteroidEventBus>();
    }

    /// <summary>
    /// on enable: set size of asteroid
    /// might need to put this in disable
    /// </summary>
    private void OnEnable()
    {
        //random choice between med and big asteroids
        //TODO: FIND BETTER WAY OF DOING THIS
        float size = Random.Range(0f, 1f);
        if (size <= _chanceForBig)
        {
            Debug.Log("2");
            gameObject.name = "Big Asteroid";
            _enemyAsteroidEventBus.Publish(EnemyAsteroidSizeEnum.BIG);
            /*
            _enemyAsteroidHealth.SetBig();
            _enemyAsteroidModel.SetBig();
            _enemyAsteroidDamage.SetBig();*/


        }
        else
        {
            Debug.Log("2");
            gameObject.name = "Medium Asteroid";
            _enemyAsteroidEventBus.Publish(EnemyAsteroidSizeEnum.MEDIUM);
            /*
            _enemyAsteroidHealth.SetMedium();
            _enemyAsteroidModel.SetMedium();
            _enemyAsteroidDamage.SetMedium();*/
        }
    }

    /// <summary>
    /// On deiable: resets asteroid
    /// </summary>
    private void OnDisable()
    {
        //ResetAsteroid();
    }

    /// <summary>
    /// returns object to pool
    /// </summary>
    public void ReturnToPool()
    {
        Pool.Release(this);
    }
}
