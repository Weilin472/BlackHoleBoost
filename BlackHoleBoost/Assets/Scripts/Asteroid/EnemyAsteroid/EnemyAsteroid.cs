using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

/*
 * Author: [Lam, Justin]
 * Last Updated: [10/31/2024]
 * [Script for enemy asteroids
 * MAKE SURE THIS IS BOTTOM OF SCRIPT COPONENTS SO ON ENABLE WORKS RIGH]
 */

public class EnemyAsteroid : MonoBehaviour
{
    private EnemyAsteroidEventBus _enemyAsteroidEventBus;

    public IObjectPool<EnemyAsteroid> Pool { get; set; }

    [SerializeField][Range(0, 1)]
    private float _chanceForBig = 0.5f;

    private SmallAsteroidType _dropType;

    /// <summary>
    /// on Start: get needed scripts
    /// </summary>
    private void Awake()
    {
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
            gameObject.name = "Big Asteroid";
            _enemyAsteroidEventBus.Publish(EnemyAsteroidSizeEnum.BIG);


        }
        else
        {
            gameObject.name = "Medium Asteroid";
            _enemyAsteroidEventBus.Publish(EnemyAsteroidSizeEnum.MEDIUM);
        }
        //Debug.Log(_dropType);
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

    /// <summary>
    /// property for drop type
    /// </summary>
    public SmallAsteroidType dropType
    {
        get { return _dropType; }
        set { _dropType = value; }
    }
}
