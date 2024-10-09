using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minotaur : EnemyBase
{
    [SerializeField] private float _detectPlayerDistance;
    private bool _isLockingOnPlayer;
    private float _timeLockOnPlayer;
    private float _currentTimeLockOnPlayer;
    private float _ramingSpeed;
    private bool _isRaming;
   [SerializeField] private float _timeLockOnPlayrMultiplier;
   [SerializeField] private float __ramingSpeedMultiplier;

    PlayerControl target;

    protected override void Start()
    {
        base.Start();
        if (GameManager.Instance._inPrototype)
        {
            _ramingSpeed = GameManager.Instance.players[0].GetSpaceShipMaxSpeed() * __ramingSpeedMultiplier;
        }
        else
        {
            _ramingSpeed = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>().GetSpaceShipMaxSpeed() * __ramingSpeedMultiplier;
        }
        _isLockingOnPlayer = false;
        _isRaming = false;
    }
    protected override void Movement()
    {
        if (!_isLockingOnPlayer && !_isRaming)
        {

            base.Movement();
            transform.rotation = Quaternion.LookRotation(Vector3.forward, _rigid.velocity);
            if (GameManager.Instance._inPrototype)
            {
                target = GameManager.Instance.GetPlayerWithMoreHealth();
            }
            else
            {
                target = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
            }
            float dis = Vector3.Distance(target.transform.position, transform.position);
            if (dis < _detectPlayerDistance)
            {
                _timeLockOnPlayer = dis / _ramingSpeed * _timeLockOnPlayrMultiplier;
                _currentTimeLockOnPlayer = 0;
                _rigid.velocity = Vector3.zero;
                _isLockingOnPlayer = true;
                target.SetLockOnIcon(true);
            }
        }
    }

    private void Update()
    {
        if (!isStuck)
        {
            if (_isLockingOnPlayer && !_isRaming)
            {
                Vector3 lastPosOfPlayer = target.transform.position;
                Vector3 dir = (lastPosOfPlayer - transform.position).normalized;
                transform.rotation = Quaternion.LookRotation(Vector3.forward, dir);
                _currentTimeLockOnPlayer += Time.deltaTime;
                if (_currentTimeLockOnPlayer > _timeLockOnPlayer)
                {

                    _rigid.velocity = dir * _ramingSpeed;
                    _currentTimeLockOnPlayer = 0;
                    _isLockingOnPlayer = false;
                    _isRaming = true;
                    target.SetLockOnIcon(false);
                }
            }
            else if (_isRaming && DetectBoundaries())
            {
                _isRaming = false;
            }
        }
    }

   
   
}
