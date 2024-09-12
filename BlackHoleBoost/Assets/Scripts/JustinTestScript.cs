using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JustinTestScript : MonoBehaviour
{
    private EnemyAsteroidPool _enemyPool;
    private PickupSmallAsteroidPool _pickupPool;

    private void Start()
    {
        _enemyPool = GetComponent<EnemyAsteroidPool>();
        _pickupPool = GetComponent<PickupSmallAsteroidPool>();
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
    }
}
