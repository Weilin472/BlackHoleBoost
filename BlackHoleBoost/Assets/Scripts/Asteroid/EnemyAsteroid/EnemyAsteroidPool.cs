using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

/*
 * Author: [Lam, Justin]
 * Last Updated: [11/03/24]
 * [object pool for big asteroids]
 */

public class EnemyAsteroidPool : MonoBehaviour
{
    [SerializeField] private GameObject _asteroidPrefab;
    private List<EnemyAsteroid> _currentEnemyAsteroids;

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

    private void OnEnable()
    {
        _currentEnemyAsteroids = new List<EnemyAsteroid>();
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
        _currentEnemyAsteroids.Remove(asteroid);
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

    /// <summary>
    /// spawns enemy asteroid for tutorial
    /// </summary>
    /// <param name="spawnLoc">location of asteroid</param>
    /// <param name="dir"></param>
    public EnemyAsteroid TutorialSpawn(Vector3 spawnLoc)
    {
        var asteroid = Pool.Get();
        asteroid.transform.position = spawnLoc;
        asteroid.GetComponent<AsteroidMove>().ChangeSpeed(0);
        //float dropSize = Random.Range(0f, 1f);

        _currentEnemyAsteroids.Add(asteroid);
        return asteroid;
    }

    /// <summary>
    /// spawns asteroid
    /// </summary>
    /// <param name="spawnLoc">location of asteroid</param>
    /// <param name="dir">move direction</param>
    public void Spawn(Vector3 spawnLoc, Vector3 dir)
    {
        var asteroid = Pool.Get();
        asteroid.transform.position = spawnLoc;
        asteroid.GetComponent<AsteroidMove>().ChangeDirection(dir);
        //float dropSize = Random.Range(0f, 1f);

        _currentEnemyAsteroids.Add(asteroid);
    }

    /// <summary>
    /// returns all asteroids
    /// </summary>
    public void ReturnAllEnemyAsteroids()
    {
        for (int i = _currentEnemyAsteroids.Count-1; i >= 0; i--)
        {
            _currentEnemyAsteroids[i].ReturnToPool();
        }
    }
}
