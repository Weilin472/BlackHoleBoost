using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [10/28/2024]
 * [movement script for asteroids]
 */

public class AsteroidMove : MonoBehaviour
{
    [SerializeField] protected float _speed = 5;
    protected Rigidbody _rigidbody;

    //testing. direction probably set when spawned
    protected Vector3 _direction;

    protected void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        //Debug.Log("teporary default is to move up change later");
        _rigidbody.velocity = _direction * _speed;
    }

    public void ChangeDirection(Vector3 dir)
    {
        _direction = new Vector3(dir.x, dir.y, 0);
        _direction.Normalize();
        _rigidbody.velocity = _direction * _speed;
    }

    public void ChangeSpeed(float speed)
    {
        _speed = speed;
        _rigidbody.velocity = _direction * _speed;
    }
}
