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
    [SerializeField] private GameObject _orbitCenter;

    private PickupSmallAsteroidPool _pickupSmallAsteroidPool;
    private PickupSmallAsteroid[] _currentAsteroids;

    private void OnEnable()
    {
        _pickupSmallAsteroidPool = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PickupSmallAsteroidPool>();
        _currentAsteroids = new PickupSmallAsteroid[_numOfAsteroid];
    }

    private void Start()
    {
        SpawnAsteroids();
    }

    private void Update()
    {
        _orbitCenter.transform.Rotate(0, 0, -_rotationSpeed * Time.deltaTime);
    }

    private void SpawnAsteroids()
    {
        for (int i = 1; i <= _numOfAsteroid; i++)
        {
            float theta = (2f * 3.14f / _numOfAsteroid) * i;

            float xPos = _radiusOfOrbit * Mathf.Cos(theta) + transform.position.x;
            float yPos = _radiusOfOrbit * Mathf.Sin(theta) + transform.position.y;

            Vector3 pos = new Vector3(xPos, yPos, 0);
            PickupSmallAsteroid temp = _pickupSmallAsteroidPool.PlanetSpawn(pos, _planetType);

            temp.transform.parent = _orbitCenter.transform;
        }
    }
}
