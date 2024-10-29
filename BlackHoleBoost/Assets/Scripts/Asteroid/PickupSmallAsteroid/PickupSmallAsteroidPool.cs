using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

/*
 * Author: [Lam, Justin]
 * Last Updated: [10/28/2024]
 * [object pooling for asteroid pickups]
 */

public class PickupSmallAsteroidPool : MonoBehaviour
{
    [SerializeField] private GameObject _asteroidPrefab;
    private List<PickupSmallAsteroid> _currentPickupAsteroids;

    public int maxPoolSize = 10;
    public int stackDefaultCapacity = 10;
    private IObjectPool<PickupSmallAsteroid> _pool;

    private Vector3 topRight;
    private Vector3 bottomLeft;

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

        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);

        topRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
        bottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, 0f));
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
        Vector3 screenPos = Camera.main.WorldToScreenPoint(pos);
        if (screenPos.x >= 0 && screenPos.x <= Screen.width && screenPos.y <= Screen.height && screenPos.y >= 0)
        {
            var asteroid = Pool.Get();
            asteroid.transform.position = pos;
            asteroid.gameObject.GetComponent<PickupSmallAsteroidMove>().magnet = true;
            asteroid.SetAsteroid();
            _currentPickupAsteroids.Add(asteroid);
        }
    }

    /// <summary>
    /// spawn pickup asteroids for planets
    /// </summary>
    /// <param name="pos">location of spawn</param>
    /// <param name="type">type of astereoid spawned</param>
    /// <returns>spawned asteriod</returns>
    public PickupSmallAsteroid PlanetSpawn(Vector3 pos, SmallAsteroidType type)
    {
        var asteroid = Pool.Get();
        asteroid.transform.position = pos;
        asteroid.SetAsteroid(type);
        _currentPickupAsteroids.Add(asteroid);
        asteroid.gameObject.GetComponent<PickupSmallAsteroidMove>().magnet = true;
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
