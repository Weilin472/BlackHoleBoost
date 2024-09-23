using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

/*
 * Author: [Lam, Justin]
 * Last Updated: [09/11/2024]
 * [object pooling for asteroid pickups]
 */

public class PickupSmallAsteroidPool : MonoBehaviour
{
    [SerializeField] private GameObject _asteroidPrefab;

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

    private PickupSmallAsteroid CreatedPooledItem()
    {
        GameObject asteroid = Instantiate(_asteroidPrefab, Vector3.zero, Quaternion.identity);
        PickupSmallAsteroid pickupAsteroid = asteroid.GetComponent<PickupSmallAsteroid>();
        pickupAsteroid.Pool = Pool;
        return pickupAsteroid;
    }

    private void OnReturnedToPool(PickupSmallAsteroid asteroid)
    {
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
    public void Spawn(Vector3 pos)
    {
        var asteroid = Pool.Get();
        asteroid.transform.position = pos;
    }
}
