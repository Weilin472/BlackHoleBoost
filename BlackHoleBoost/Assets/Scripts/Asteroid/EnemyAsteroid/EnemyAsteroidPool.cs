using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

/*
 * Author: [Lam, Justin]
 * Last Updated: [09/20/24]
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
    public void TestSpawn()
    {
        var asteroid = Pool.Get();
        Debug.Log("REMEMBER TO ADD BACK ENEMY MOVE ADD BACK ZERO");
        //asteroid.transform.position = Vector3.zero;
        Vector3 pos = new Vector3(0, 5, 0);
        asteroid.transform.position = pos;
    }

    public void Spawn(Vector3 spawnLoc, Vector3 dir)
    {
        var asteroid = Pool.Get();
        asteroid.transform.position = spawnLoc;
        asteroid.GetComponent<AsteroidMove>().ChangeDirection(dir);

    }
}
