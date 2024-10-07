using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [09/16/2024]
 * [movement script for asteroids]
 */

public class AsteroidMove : MonoBehaviour
{
    [SerializeField] private float _speed = 5;
    private Rigidbody _rigidbody;
    
    //testing. direction probably set when spawned
    private Vector3 _direction;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        //Debug.Log("teporary default is to move up change later");
        _rigidbody.velocity = _direction * _speed;
    }

    public void ChangeDirection(Vector3 dir)
    {
        Vector3 direction = new Vector3(dir.x, dir.y, 0);
        direction.Normalize();
        _rigidbody.velocity = direction * _speed;
    }
}
