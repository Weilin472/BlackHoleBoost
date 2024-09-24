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
    private List<GameObject> _currentEnemies;
    private int _currentIndex = 0;

    private bool _isSpawning = false;
    private void OnEnable()
    {
        _currentEnemies = new List<GameObject>();
    }

    private void Update()
    {
        
    }
    public void StartSpawning()
    {
        _isSpawning = true;
    }

    public void StopSpawning()
    {
        _isSpawning = false;
    }

    private void SpawnEnemy()
    {
        if (_currentIndex +1 > _enemyPrefabs.Length)
        {
            _currentIndex = 0;
        }
        else
        {
            _currentIndex++;
        }

        Vector3 loc = new Vector3(Random.Range(-2, 2), 5, 0);
        Instantiate(_enemyPrefabs[_currentIndex], loc, Quaternion.identity);
    }
}
