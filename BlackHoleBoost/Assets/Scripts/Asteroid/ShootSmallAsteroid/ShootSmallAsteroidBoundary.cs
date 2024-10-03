using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [09/29/2024]
 * [script to change how the asteroid interacts with the barrier]
 */

public class ShootSmallAsteroidBoundary : MonoBehaviour
{
    private ShootSmallAsteroid _shootSmallAsteroid;
    private ShootSmallAsteroidEventBus _shootSmallAsteroidEventBus;

    //delegates for what happens when asteroid goes out of bounds
    delegate void AsteroidEffect(Vector3 pos);
    private AsteroidEffect _asteroidEffect;

    //rigidbody to track speed and direction
    private Rigidbody _rigidbody;

    //screen warp vars
    private Vector3 _screenPos;
    private Vector3 _topRight;
    private Vector3 _bottomLeft;

    /// <summary>
    /// gets needed components
    /// </summary>
    private void Awake()
    {
        _shootSmallAsteroidEventBus = GetComponent<ShootSmallAsteroidEventBus>();
        _shootSmallAsteroid = GetComponent<ShootSmallAsteroid>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// subscribes to event bus
    /// </summary>
    private void OnEnable()
    {
        _topRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
        _bottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, 0f));


        _shootSmallAsteroidEventBus.Subscribe(SmallAsteroidType.NORMAL, SetScreenWarp);
        _shootSmallAsteroidEventBus.Subscribe(SmallAsteroidType.BOUNCE, SetScreenWarp);
        _shootSmallAsteroidEventBus.Subscribe(SmallAsteroidType.STICKY, SetBoundaryDestroy);
    }

    /// <summary>
    /// unsubscribes from event bus
    /// </summary>
    private void OnDisable()
    {
        _asteroidEffect = null;
        _shootSmallAsteroidEventBus.Unsubscribe(SmallAsteroidType.NORMAL, SetScreenWarp);
        _shootSmallAsteroidEventBus.Unsubscribe(SmallAsteroidType.BOUNCE, SetScreenWarp);
        _shootSmallAsteroidEventBus.Unsubscribe(SmallAsteroidType.STICKY, SetBoundaryDestroy);
    }

    /// <summary>
    /// checks every frame for the asteroid to go past the boundaries
    /// </summary>
    private void Update()
    {
        _screenPos = Camera.main.WorldToScreenPoint(transform.position);
        bool hitBoundary = false;
        if (_screenPos.x <= -200 && _rigidbody.velocity.x < 0)
        {
            _asteroidEffect(new Vector3(_topRight.x + 2, transform.position.y, 0f));
            hitBoundary = true;
        }
        else if (_screenPos.x >= Screen.width + 200 && _rigidbody.velocity.x > 0)
        {
            _asteroidEffect(new Vector3(_bottomLeft.x - 2, transform.position.y, 0f));
            hitBoundary = true;
        }
        if (_screenPos.y >= Screen.height + 200 && _rigidbody.velocity.y > 0)
        {
            _asteroidEffect(new Vector3(transform.position.x, _bottomLeft.y - 2, 0f));
            hitBoundary = true;
        }
        else if (_screenPos.y <= -200 && _rigidbody.velocity.y < 0)
        {
            _asteroidEffect(new Vector3(transform.position.x, _topRight.y + 2, 0f));
            hitBoundary = true;
        }

        if (PlaytestData.Instance != null && hitBoundary)
        {
            PlaytestData.Instance.numberOfShootAsteroidReachingBarrier++;
        }
    }

    /// <summary>
    /// sets the delegate to screen warp
    /// </summary>
    private void SetScreenWarp()
    {
        _asteroidEffect += ScreenWarp;
    }

    /// <summary>
    /// sets the delegeate to destroy
    /// </summary>
    private void SetBoundaryDestroy()
    {
        _asteroidEffect += BoundaryDestroy;
    }

    /// <summary>
    /// screen warps the asteroid
    /// </summary>
    /// <param name="pos">where the asteroid should move</param>
    private void ScreenWarp(Vector3 pos)
    {
        transform.position = pos;
    }

    /// <summary>
    /// returns the asteroid to the pool
    /// </summary>
    /// <param name="pos">so it works with the delegate</param>
    private void BoundaryDestroy(Vector3 pos)
    {
        _shootSmallAsteroid.ReturnToPool();
    }
}
