using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [9/20/2024]
 * [Any gameobject with this script, will wrap around the screen]
 */

public class EnemyAsteroidSpawner : MonoBehaviour
{
    private EnemyAsteroidPool _enemyAsteroidPool;

    public bool testStart = false;
    public bool testStop = false;

    [SerializeField] private float _spawnRate = 5;
    private IEnumerator _spawnCoroutine;
    private bool _isSpawning = false;

    private void Awake()
    {
        _enemyAsteroidPool = GetComponent<EnemyAsteroidPool>();
        _spawnCoroutine = SpawnAsteroidCoroutine();
    }

    private void Update()
    {
        if (testStart)
        {
            StartSpawning();
            testStart = false;
        }
        if (testStop)
        {
            StopSpawning();
            testStop = false;
        }
    }

    public void StartSpawning()
    {
        _isSpawning = true;
        StartCoroutine(_spawnCoroutine);
    }

    public void StopSpawning()
    {
        _isSpawning = false;
        StopCoroutine(_spawnCoroutine);
    }

    public void SpawnAsteroid()
    {
        Vector3 topRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
        Vector3 bottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, 0f));

        Vector3 direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
        direction.Normalize();

        Vector3 loc = Vector3.zero;
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            loc = new Vector3(bottomLeft.x - 2, Random.Range(bottomLeft.y, topRight.y), 0);
        }
        else
        {
            loc = new Vector3(Random.Range(bottomLeft.x, topRight.x), bottomLeft.y - 2, 0);
        }

        _enemyAsteroidPool.Spawn(loc, direction);
    }

    private IEnumerator SpawnAsteroidCoroutine()
    {
        while (_isSpawning)
        {
            yield return new WaitForSeconds(_spawnRate);
            SpawnAsteroid();
        }
    }
}
