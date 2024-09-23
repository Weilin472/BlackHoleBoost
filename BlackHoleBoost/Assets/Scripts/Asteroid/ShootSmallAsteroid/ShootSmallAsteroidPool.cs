using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

/*
 * Author: [Lam, Justin]
 * Last Updated: [09/16/2024]
 * [object pooling for shooting asteroid]
 */

public class ShootSmallAsteroidPool : MonoBehaviour
{
    [SerializeField] private GameObject _asteroidPrefab;

    public int maxPoolSize = 10;
    public int stackDefaultCapacity = 10;
    private IObjectPool<ShootSmallAsteroid> _pool;

    public IObjectPool<ShootSmallAsteroid> Pool
    {
        get
        {
            if (_pool == null)
            {
                _pool =
                    new ObjectPool<ShootSmallAsteroid>(
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

    private ShootSmallAsteroid CreatedPooledItem()
    {
        GameObject asteroid = Instantiate(_asteroidPrefab, Vector3.zero, Quaternion.identity);
        ShootSmallAsteroid shootAsteroid = asteroid.GetComponent<ShootSmallAsteroid>();
        shootAsteroid.Pool = Pool;
        return shootAsteroid;
    }

    private void OnReturnedToPool(ShootSmallAsteroid asteroid)
    {
        asteroid.gameObject.SetActive(false);
    }

    private void OnTakeFromPool(ShootSmallAsteroid asteroid)
    {
        asteroid.gameObject.SetActive(true);
    }

    private void OnDestroyPoolObject(ShootSmallAsteroid asteroid)
    {
        Destroy(asteroid);
    }

    /// <summary>
    /// used to test spawn
    /// </summary>
    public void TestSpawn(SmallAsteroidType asteroidType)
    {
        var asteroid = Pool.Get();
        asteroid.GetComponent<ShootSmallAsteroid>().SetAsteroid(asteroidType);
        asteroid.transform.position = Vector3.zero;
    }

    /// <summary>
    /// shoots asteroid
    /// </summary>
    /// <param name="asteroidType">what type of asteroid</param>
    /// <param name="spawnLoc">where to spawn asteroid</param>
    /// <param name="dir">what direction does the asteroid go</param>
    public void Spawn(SmallAsteroidType asteroidType, Vector3 spawnLoc, Vector3 dir)
    {
        var asteroid = Pool.Get();
        asteroid.GetComponent<ShootSmallAsteroid>().SetAsteroid(asteroidType);
        asteroid.transform.position = spawnLoc;
        asteroid.GetComponent<AsteroidMove>().ChangeDirection(dir);
    }
}
