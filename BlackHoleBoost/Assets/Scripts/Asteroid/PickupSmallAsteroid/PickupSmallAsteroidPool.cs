using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

/*
 * Author: [Lam, Justin]
 * Last Updated: [09/14/2024]
 * [object pooling for asteroid pickups]
 */

public class PickupSmallAsteroidPool : MonoBehaviour
{
    [SerializeField] private GameObject _asteroidPrefab;
    private List<PickupSmallAsteroid> _currentPickupAsteroids;

    public int maxPoolSize = 10;
    public int stackDefaultCapacity = 10;
    private IObjectPool<PickupSmallAsteroid> _pool;

    public IObjectPool<PickupSmallAsteroid> Pool
    {
        get
        {
            if (_pool == null)
            {
                _pool =
                    new ObjectPool<PickupSmallAsteroid>(
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

    /// <summary>
    /// makes list
    /// </summary>
    private void Awake()
    {
        _currentPickupAsteroids = new List<PickupSmallAsteroid>();
    }

    private PickupSmallAsteroid CreatedPooledItem()
    {
        GameObject asteroid = Instantiate(_asteroidPrefab, Vector3.zero, Quaternion.identity);
        PickupSmallAsteroid pickupAsteroid = asteroid.GetComponent<PickupSmallAsteroid>();
        pickupAsteroid.Pool = Pool;
        return pickupAsteroid;
    }

    private void OnReturnedToPool(PickupSmallAsteroid asteroid)
    {
        _currentPickupAsteroids.Remove(asteroid);
        asteroid.gameObject.transform.parent = null;
        asteroid.gameObject.SetActive(false);
    }

    private void OnTakeFromPool(PickupSmallAsteroid asteroid)
    {
        asteroid.gameObject.SetActive(true);
    }

    private void OnDestroyPoolObject(PickupSmallAsteroid asteroid)
    {
        Destroy(asteroid);
    }

    /// <summary>
    /// TODO: ASTEROID SPAWN location
    /// </summary>
    public void TestSpawn()
    {
        var asteroid = Pool.Get();
        asteroid.transform.position = Vector3.zero;
    }

    /// <summary>
    /// spawns asteroid
    /// </summary>
    /// <param name="pos">position of asteroid</param>
    public void Spawn(Vector3 pos)
    {
        var asteroid = Pool.Get();
        asteroid.transform.position = pos;
        asteroid.SetAsteroid();
        _currentPickupAsteroids.Add(asteroid);
    }


    public PickupSmallAsteroid PlanetSpawn(Vector3 pos, SmallAsteroidType type)
    {
        var asteroid = Pool.Get();
        asteroid.transform.position = pos;
        asteroid.SetAsteroid(type);
        _currentPickupAsteroids.Add(asteroid);
        return asteroid;
    }

    /// <summary>
    /// returns all asteroids
    /// </summary>
    public void ReturnAllPickupAsteroids()
    {
        for (int i = _currentPickupAsteroids.Count - 1; i >= 0; i--)
        {
            _currentPickupAsteroids[i].ReturnToPool();
        }
    }
}
