using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [09/20/2024]
 * [test script to spawn asteroid]
 */

public class JustinTestScript : MonoBehaviour
{
    private EnemyAsteroidPool _enemyPool;
    private PickupSmallAsteroidPool _pickupPool;
    private ShootSmallAsteroidPool _shootPool;

    private void Start()
    {
        _enemyPool = GetComponent<EnemyAsteroidPool>();
        _pickupPool = GetComponent<PickupSmallAsteroidPool>();
        _shootPool = GetComponent<ShootSmallAsteroidPool>();
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Spawn Enemy Asteroid"))
        {
            _enemyPool.TestSpawn();
        }
        if (GUILayout.Button("Spawn Pickup Asteroid"))
        {
            _pickupPool.TestSpawn();
        }
        if (GUILayout.Button("Spawn Normal Shoot Asteroid"))
        {
            _shootPool.TestSpawn(SmallAsteroidType.NORMAL);
        }
        if (GUILayout.Button("Spawn Bounce Shoot Asteroid"))
        {
            _shootPool.TestSpawn(SmallAsteroidType.BOUNCE);
        }
        if (GUILayout.Button("Spawn Sticky Shoot Asteroid"))
        {
            _shootPool.TestSpawn(SmallAsteroidType.STICKY);
        }
    }
}
