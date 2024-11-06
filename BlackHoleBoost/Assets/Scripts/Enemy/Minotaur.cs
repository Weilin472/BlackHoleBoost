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


    protected override void Start()
    {
        base.Start();
        if (GameManager.Instance._inPrototype)
        {
            _ramingSpeed = GameManager.Instance.players[0].GetSpaceShipMaxSpeed() * __ramingSpeedMultiplier;
        }
        else
        {
            if (GameObject.FindGameObjectWithTag("Player"))
            {
                _ramingSpeed = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>().GetSpaceShipMaxSpeed() * __ramingSpeedMultiplier;
            }
            
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
            float dis = Vector3.Distance(targetPlayer.transform.position, transform.position);
            if (dis < _detectPlayerDistance)
            {
                _timeLockOnPlayer = dis / _ramingSpeed * _timeLockOnPlayrMultiplier;
                _currentTimeLockOnPlayer = 0;
                _rigid.velocity = Vector3.zero;
                _isLockingOnPlayer = true;
                targetPlayer.SetLockOnIcon(true);
            }
        }
    }

    protected override void Update()
    {
        base.Update();
        if (!isStuck)
        {
            if (_isLockingOnPlayer && !_isRaming)
            {
                Vector3 lastPosOfPlayer = targetPlayer.transform.position;
                Vector3 dir = (lastPosOfPlayer - transform.position).normalized;
                transform.rotation = Quaternion.LookRotation(Vector3.forward, dir);
                _currentTimeLockOnPlayer += Time.deltaTime;
                if (_currentTimeLockOnPlayer > _timeLockOnPlayer)
                {

                    _rigid.velocity = dir * _ramingSpeed;
                    _currentTimeLockOnPlayer = 0;
                    _isLockingOnPlayer = false;
                    _isRaming = true;
                    targetPlayer.SetLockOnIcon(false);
                }
            }
            else if (_isRaming && DetectBoundaries())
            {
                _rigid.velocity = Vector3.zero;
                _isRaming = false;
            }
        }
    }

   
   
}
