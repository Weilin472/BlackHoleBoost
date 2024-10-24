using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [09/24/2024]
 * [enemy spawner for prototype]
 */

public class PrototypeEnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemyPrefabs;
    private GameObject _currentEnemy;

    private bool _isSpawning = false;

    /// <summary>
    /// spawns enemy when no enemy
    /// </summary>
    private void Update()
    {
        if (_isSpawning && _currentEnemy == null)
        {
            SpawnEnemy();
        }
    }

    /// <summary>
    /// start spawning
    /// </summary>
    public void StartSpawning()
    {
        _isSpawning = true;
    }

    /// <summary>
    /// stop spawning
    /// </summary>
    public void StopSpawning()
    {
        _isSpawning = false;
        if (_currentEnemy != null)
        {
            Destroy(_currentEnemy);
        }
    }

    /// <summary>
    /// spawns enemy
    /// </summary>
    private void SpawnEnemy()
    {
        Vector3 loc = new Vector3(Random.Range(-2, 2), 10, 0);
        int spawnIndex = Random.Range(0, _enemyPrefabs.Length);
        _currentEnemy = Instantiate(_enemyPrefabs[spawnIndex], loc, Quaternion.identity);


        if (PlaytestDataCollector.Instance != null)
        {
            PlaytestDataCollector.Instance.numberOfEnemySpawns++;
            if (spawnIndex == 0)
            {
                PlaytestDataCollector.Instance.cyclopsSpawned++;
            }
            else if (spawnIndex == 1)
            {
                PlaytestDataCollector.Instance.minotaurSpawned++;
            }
        }

    }
}
