using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hydra : EnemyBase
{
    [SerializeField] private float movingTime;
    private float _currentMoveTime;
    private bool _isMoving;

    protected override void Start()
    {
        base.Start();
        _rigid.velocity = transform.TransformDirection(Vector3.up) * _speed;
        _isMoving = true;
    }

    protected override void Movement()
    {
       
    }

    protected override void Update()
    {
        base.Update();
        if (_isMoving)
        {
            if (DetectBoundaries())
            {
                _rigid.velocity *= -1;
                transform.rotation = Quaternion.LookRotation(transform.forward, _rigid.velocity);
            }
            _currentMoveTime += Time.deltaTime;
            if (_currentMoveTime >= movingTime)
            {
                _currentMoveTime = 0;
                _isMoving = false;
                _rigid.velocity = Vector3.zero;
                int stopTime = Random.Range(5, 11);
                Invoke("ResetMovement",stopTime);
            }
        }     
    }

    private void ResetMovement()
    {
        _isMoving = true;
        int rotateAngle = Random.Range(0, 361);
        transform.Rotate(0, 0, rotateAngle);
        _rigid.velocity = transform.TransformDirection(Vector3.up) * _speed;

    }
}
