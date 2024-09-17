using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [09/14/2024]
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
            _enemyPool.Spawn();
        }
        if (GUILayout.Button("Spawn Pickup Asteroid"))
        {
            _pickupPool.Spawn();
        }
        if (GUILayout.Button("Spawn Normal Shoot Asteroid"))
        {
            _shootPool.Spawn(SmallAsteroidType.NORMAL);
        }
        if (GUILayout.Button("Spawn Bounce Shoot Asteroid"))
        {
            _shootPool.Spawn(SmallAsteroidType.BOUNCE);
        }
        if (GUILayout.Button("Spawn Sticky Shoot Asteroid"))
        {
            _shootPool.Spawn(SmallAsteroidType.STICKY);
        }
    }
}
