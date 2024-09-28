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

    private void Update()
    {
        if (_isSpawning && _currentEnemy == null)
        {
            SpawnEnemy();
        }
    }

    public void StartSpawning()
    {
        _isSpawning = true;
    }

    public void StopSpawning()
    {
        _isSpawning = false;
        if (_currentEnemy != null)
        {
            Destroy(_currentEnemy);
        }
    }

    private void SpawnEnemy()
    {
        Vector3 loc = new Vector3(Random.Range(-2, 2), 10, 0);
        _currentEnemy = Instantiate(_enemyPrefabs[Random.Range(0, _enemyPrefabs.Length)], loc, Quaternion.identity);
    }
}
