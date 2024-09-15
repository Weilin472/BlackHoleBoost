using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (GUILayout.Button("Spawn Shoot Asteroid"))
        {
            _shootPool.Spawn();
        }
    }
}
