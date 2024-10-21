using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [10/14/2024]
 * [spawns and rotates asteroids around planet]
 */

public class PickupSmallAsteroidPlanet : MonoBehaviour
{
    [SerializeField] private SmallAsteroidType _planetType = SmallAsteroidType.NORMAL;
    [SerializeField] private float _rotationSpeed = 22.5f;
    [SerializeField] private float _radiusOfOrbit = 2;
    [SerializeField] private int _numOfAsteroid = 5;
    [SerializeField] private float _timeToRespawn = 5f;
    [SerializeField] private GameObject _orbitCenter;

    private PickupSmallAsteroidPool _pickupSmallAsteroidPool;
    private PickupSmallAsteroid[] _currentAsteroids;
    private GameObject[] _asteroidLocations;

    private void OnEnable()
    {
        _pickupSmallAsteroidPool = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PickupSmallAsteroidPool>();
        _currentAsteroids = new PickupSmallAsteroid[_numOfAsteroid];
        _asteroidLocations = new GameObject[_numOfAsteroid];
    }

    private void Start()
    {
        SpawnAsteroids();
        StartCoroutine(RespawnAsteroids());
    }

    private void Update()
    {
        _orbitCenter.transform.Rotate(0, 0, -_rotationSpeed * Time.deltaTime);
        for (int i = 0; i < _currentAsteroids.Length; i++)
        {
            if (_currentAsteroids[i] != null && !_currentAsteroids[i].gameObject.activeSelf)
            {
                _currentAsteroids[i] = null;
            }
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < _currentAsteroids.Length; i++)
        {
            if (_currentAsteroids[i] != null)
            {
                //replace with wander
                _currentAsteroids[i].ReturnToPool();
            }
        }
    }

    private void SpawnAsteroids()
    {
        for (int i = 1; i <= _numOfAsteroid; i++)
        {
            float theta = (2f * 3.14f / _numOfAsteroid) * i;

            float xPos = _radiusOfOrbit * Mathf.Cos(theta) + transform.position.x;
            float yPos = _radiusOfOrbit * Mathf.Sin(theta) + transform.position.y;

            Vector3 pos = new Vector3(xPos, yPos, 0);
            _asteroidLocations[i - 1] = new GameObject("LocationTracker");
            _asteroidLocations[i - 1].transform.parent = _orbitCenter.transform;
            _asteroidLocations[i - 1].transform.position = pos;

            PickupSmallAsteroid temp = _pickupSmallAsteroidPool.PlanetSpawn(pos, _planetType);

            temp.transform.parent = _orbitCenter.transform;
            _currentAsteroids[i - 1] = temp;
        }
    }

    private IEnumerator RespawnAsteroids()
    {
        while (true)
        {
            for (int i = 0; i < _currentAsteroids.Length; i++)
            {
                if (_currentAsteroids[i] == null)
                {
                    PickupSmallAsteroid temp = _pickupSmallAsteroidPool.PlanetSpawn(_asteroidLocations[i].transform.position, _planetType);

                    temp.transform.parent = _orbitCenter.transform;
                    _currentAsteroids[i] = temp;
                }
            }

            yield return new WaitForSeconds(_timeToRespawn);
        }
    }
}