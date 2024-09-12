using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [09/05/2024]
 * [movement script for asteroids]
 */

public class AsteroidMove : MonoBehaviour
{
    [SerializeField] private float _speed = 5;
    private Rigidbody _rigidbody;
    
    //testing. direction probably set when spawned
    [SerializeField] private Vector3 _direction = Vector3.up;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.velocity = _direction * _speed;
    }
}
