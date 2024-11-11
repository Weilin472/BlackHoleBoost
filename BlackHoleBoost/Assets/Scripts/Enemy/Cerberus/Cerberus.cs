using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cerberus : EnemyBase
{
    [SerializeField] private float _guardSpeed;
    [SerializeField] private float _pursuitSpeed;
    [SerializeField] private float _distanceToPursuit;

    private bool _pursuit = false;

    private Vector3 _guardPos;
    private Vector3 _nextPos;

    /// <summary>
    /// sets the speed
    /// sets a random guard position
    /// </summary>
    private void OnEnable()
    {
        _speed = _guardSpeed;
        float[] xPos = { -4.25f, 0f, 4.25f };

        _guardPos = new Vector3(xPos[Random.Range(0, 3)], 0, 0);
        _nextPos = _guardPos;
    }

    /// <summary>
    /// only moves to player when in persuit mode
    /// </summary>
    protected override void Movement()
    {
        if (_pursuit)
        {
            base.Movement();
        }
    }

    protected override void Update()
    {
        base.Update();
        CheckForPersuit();
        if (!_pursuit)
        {
            MoveToNextPos();
        }
        transform.rotation = Quaternion.LookRotation(Vector3.forward, _rigid.velocity);
    }

    private void CheckForPersuit()
    {
        Vector3 playerPos = Vector3.zero;
        if (GameManager.Instance._inPrototype)
        {
            if (GameManager.Instance.players.Count > 0)
            {
                playerPos = GameManager.Instance.GetPlayerWithMoreHealth().transform.position;
            }
        }
        else
        {
            if (GameObject.FindGameObjectWithTag("Player"))
            {
                playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
            }


        }

        float dist = Vector3.Distance(transform.position, playerPos);
        if (dist <= _distanceToPursuit)
        {
            _pursuit = true;
            _speed = _pursuitSpeed;
        }
    }

    private void MoveToNextPos()
    {
        if ((_nextPos - transform.position).magnitude < .1f)
        {
            Vector3 nextLocFromGuardPos = Random.insideUnitSphere * 5;
            nextLocFromGuardPos.z = 0;
            _nextPos = _guardPos + nextLocFromGuardPos;
        }

        Vector3 dir = (_nextPos - transform.position).normalized;
        _rigid.velocity = dir * _currentSpeed;
    }
}
