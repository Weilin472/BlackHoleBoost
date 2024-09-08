using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

/*
 * Author: [Lam, Justin]
 * Last Updated: [09/07/24]
 * [object pool for big asteroids]
 */

public class EnemyAsteroidPool : MonoBehaviour
{
    [SerializeField] private GameObject _asteroidPrefab;

    public int maxPoolSize = 10;
    public int stackDefaultCapacity = 10;
    private IObjectPool<EnemyAsteroid> _pool;

    public IObjectPool<EnemyAsteroid> Pool
    {
        get
        {
            if (_pool == null)
            {
                _pool =
                    new ObjectPool<EnemyAsteroid>(
                        CreatedPooledItem,
                        OnTakeFromPool,
                        OnReturnedToPool,
                        OnDestroyPoolObject,
                        true,
                        stackDefaultCapacity,
                        maxPoolSize
                        );
            }
            return _pool;
        }
    }

    private EnemyAsteroid CreatedPooledItem()
    {
        GameObject asteroid = Instantiate(_asteroidPrefab, Vector3.zero, Quaternion.identity);
        EnemyAsteroid enemyAsteroid = asteroid.GetComponent<EnemyAsteroid>();
        enemyAsteroid.Pool = Pool;
        return enemyAsteroid;
    }

    private void OnReturnedToPool(EnemyAsteroid asteroid)
    {
        asteroid.gameObject.SetActive(false);
    }

    private void OnTakeFromPool(EnemyAsteroid asteroid)
    {
        asteroid.gameObject.SetActive(true);
    }

    private void OnDestroyPoolObject(EnemyAsteroid asteroid)
    {
        Destroy(asteroid);
    }

    /// <summary>
    /// TODO: ASTEROID SPAWN location
    /// </summary>
    public void Spawn()
    {
        var asteroid = Pool.Get();
        asteroid.transform.position = Vector3.zero;
        //drone.transform.position = Random.insideUnitSphere * 10;
    }
}
