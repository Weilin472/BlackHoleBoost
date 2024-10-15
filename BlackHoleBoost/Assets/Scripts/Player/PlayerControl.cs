using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float _accelerationMultipler;
    [SerializeField] private float _sideMoveSpeed;
    [SerializeField] private GameObject _blackHolePrefab;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _maxBlackHoleSpeed;
    [SerializeField] private GameObject _beLockedOnIcon;
    [SerializeField] private float _blackHoleSuckUpMaxTime;



    private GameObject _currentBlackHole;
    private GameObject _currentPlanet;
    private float _currentBlachHoleSpeed;
    private float _circleModeRotateDiameter;
    private Rigidbody rigid;
    private float _currentBlackHoleModeRotateSpeed;
    private float _currentTimeStayInBlackHole;

    private PlayerShoot _playerShoot;

    public bool isInBlackHole;
    private bool _canInteractWithPlanet;
    private bool _isInPlanet;
    private bool _isClockDirection;

    private bool _isMovingLeft;
    private bool _isMovingRight;
    private bool _isAccelerating;
    private bool _isSlowingDown;


   

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        _playerShoot = GetComponent<PlayerShoot>();


        Debug.Log(transform.rotation);
       
    }

    private void Update()
    {
      
        if (isInBlackHole||_isInPlanet)
        {
            if (_currentBlachHoleSpeed<_maxBlackHoleSpeed)
            {
                _currentBlachHoleSpeed +=  Time.deltaTime;
              
            }
            _currentBlackHoleModeRotateSpeed =360/(Mathf.PI * _circleModeRotateDiameter / _currentBlachHoleSpeed);

            if (_isClockDirection)
            {
                transform.Rotate(0, 0, -_currentBlackHoleModeRotateSpeed * Time.deltaTime);
            }
            else
            {
                transform.Rotate(0, 0, _currentBlackHoleModeRotateSpeed * Time.deltaTime);
            }

            if (isInBlackHole)
            {
                _currentTimeStayInBlackHole += Time.deltaTime;
                if (_currentTimeStayInBlackHole > _blackHoleSuckUpMaxTime)
                {
                    transform.GetComponent<PlayerHealthScript>().Damage(100);
                    ExitBlackHoleMode();
                    rigid.velocity = Vector3.zero;
                }
            }      
            return;
        }

       


    }

    private void FixedUpdate()
    {
        if (isInBlackHole||_isInPlanet)
        {
            rigid.velocity = transform.TransformDirection(Vector3.up)*_currentBlachHoleSpeed;
            
            return;
        }
        if (!isInBlackHole&&rigid.velocity.magnitude>_maxSpeed)
        {
            rigid.AddForce(transform.TransformDirection(Vector3.down) * _accelerationMultipler, ForceMode.Acceleration);
        }
        if (_isAccelerating)
        {
            if (Mathf.Abs(rigid.velocity.magnitude)<_maxSpeed)
            {
                rigid.AddForce(transform.TransformDirection(Vector3.up) * _accelerationMultipler, ForceMode.Acceleration);
            }
            else
            {
                Vector3 relativeVelocity = transform.InverseTransformDirection(rigid.velocity);
                relativeVelocity.y = _maxSpeed;
                rigid.velocity = transform.TransformDirection(relativeVelocity);
            }
        }
        else if (_isSlowingDown)
        {

            if (Mathf.Abs(rigid.velocity.magnitude)>_accelerationMultipler)
            {
                rigid.AddForce(transform.TransformDirection(Vector3.down) * _accelerationMultipler, ForceMode.Acceleration);
            }
            else
            {
                Vector3 relativeVelocity = transform.InverseTransformDirection(rigid.velocity);
                relativeVelocity.y = 0;
                rigid.velocity = transform.TransformDirection(relativeVelocity);
            }
        }

        if (_isMovingLeft)
        {
            //   rigid.velocity =new Vector3(-_sideMoveSpeed, rigid.velocity.y, rigid.velocity.z);
            //   rigid.velocity = transform.TransformDirection(new Vector3(-_sideMoveSpeed, rigid.velocity.y, rigid.velocity.z));
            // transform.position += transform.TransformDirection(Vector3.left * _sideMoveSpeed * Time.deltaTime);
            Vector3 relativeVelocity = transform.InverseTransformDirection(rigid.velocity);
            relativeVelocity.x = -_sideMoveSpeed;
            if (!IsNormalStrafing())
            {
                relativeVelocity.x *= -1;
            }
            rigid.velocity = transform.TransformDirection(relativeVelocity);
        }
        else if (_isMovingRight)
        {
            //   rigid.velocity =new Vector3(_sideMoveSpeed, rigid.velocity.y, rigid.velocity.z);
            //   rigid.velocity = transform.TransformDirection(new Vector3(_sideMoveSpeed, rigid.velocity.y, rigid.velocity.z));
            // transform.position += transform.TransformDirection(Vector3.right * _sideMoveSpeed * Time.deltaTime);
            Vector3 relativeVelocity = transform.InverseTransformDirection(rigid.velocity);
            relativeVelocity.x = _sideMoveSpeed;
            if (!IsNormalStrafing())
            {
                relativeVelocity.x *= -1;
            }
            rigid.velocity = transform.TransformDirection(relativeVelocity);
        }

    }

    private bool IsNormalStrafing()
    {
        float zAxis = transform.localEulerAngles.z;
        while (zAxis>180)
        {
            zAxis -= 360;
        }
        while (zAxis<-180)
        {
            zAxis += 360;
        }
        if (-90<zAxis&&zAxis<90)
        {
            return true;
        }
        return false;
    }

    public void LeftMove(InputAction.CallbackContext input)
    {
        if (input.phase==InputActionPhase.Performed)
        {
            _isMovingLeft = true;
        }
        else if (input.phase==InputActionPhase.Canceled)
        {
            _isMovingLeft = false;
            //   rigid.velocity = new Vector3(0, rigid.velocity.y, rigid.velocity.z);
            Vector3 relativeVelocity = transform.InverseTransformDirection(rigid.velocity);
            relativeVelocity.x = 0;
            rigid.velocity = transform.TransformDirection(relativeVelocity);
        }
    }

    public void RightMove(InputAction.CallbackContext input)
    {
        if (input.phase == InputActionPhase.Performed)
        {
            _isMovingRight = true;
        }
        else if (input.phase == InputActionPhase.Canceled)
        {
            _isMovingRight = false;
            //    rigid.velocity = new Vector3(0, rigid.velocity.y, rigid.velocity.z);
            Vector3 relativeVelocity = transform.InverseTransformDirection(rigid.velocity);
            relativeVelocity.x = 0;
            rigid.velocity = transform.TransformDirection(relativeVelocity);
        }
    }

    public void Acceleration(InputAction.CallbackContext input)
    {
        if (input.phase == InputActionPhase.Performed)
        {
            _isAccelerating = true;
        }
        else if (input.phase == InputActionPhase.Canceled)
        {
            _isAccelerating = false;
        }
    }

    public void SlowDown(InputAction.CallbackContext input)
    {
        if (input.phase == InputActionPhase.Performed)
        {
            _isSlowingDown = true;
        }
        else if (input.phase == InputActionPhase.Canceled)
        {
            _isSlowingDown = false;
        }
    }
    
    public void SpawnBlackHole(InputAction.CallbackContext input)
    {
        if (input.phase == InputActionPhase.Performed)
        {
            if (!isInBlackHole&&!_canInteractWithPlanet&&!_isInPlanet)//spawn black holes
            {
                if (_isMovingLeft)
                {
                    _isClockDirection = false;
                    _currentBlackHole = Instantiate(_blackHolePrefab, transform.position + transform.TransformDirection(Vector3.left) * 0.8f, Quaternion.identity);
                }
                else
                {
                    _isClockDirection = true;
                    _currentBlackHole = Instantiate(_blackHolePrefab, transform.position + transform.TransformDirection(Vector3.right) * 0.8f, Quaternion.identity);
                }
                isInBlackHole = true;
                _currentBlachHoleSpeed =Mathf.Abs(rigid.velocity.magnitude);
                _circleModeRotateDiameter = _currentBlackHole.transform.localScale.x * 0.8f;
                Collider[] colliders = Physics.OverlapSphere(_currentBlackHole.transform.position, _currentBlackHole.transform.localScale.x / 2);
                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].tag=="Planet")
                    {
                        Destroy(colliders[i].gameObject);
                    }
                }
            }
            else if (_canInteractWithPlanet&&!_isInPlanet&&!isInBlackHole)//circle the planets
            {
                _isInPlanet = true;
                _canInteractWithPlanet = false;
                _currentBlachHoleSpeed = Mathf.Abs(rigid.velocity.magnitude);
                _circleModeRotateDiameter = Vector3.Distance(_currentPlanet.transform.position, transform.position) * 2;

                Vector3 contactDir = transform.position - _currentPlanet.transform.position;
                Vector3 adjustedDir = new Vector3(-contactDir.y, contactDir.x, 0).normalized;
                float planetLocalX = transform.InverseTransformPoint(_currentPlanet.transform.position).x;
                if (planetLocalX >= 0)
                {
                    _isClockDirection = true;
                    transform.rotation = Quaternion.LookRotation(Vector3.forward, -adjustedDir);
                }
                else
                {
                    _isClockDirection = false;
                    transform.rotation = Quaternion.LookRotation(Vector3.forward, adjustedDir);
                }
            }
            else if(isInBlackHole)//destory blackhole
            {
                ExitBlackHoleMode();
            }
            else if (_isInPlanet)//leave the planet
            {
                _currentPlanet = null;
                _isInPlanet = false;
            }
        }
    }

    public void ExitBlackHoleMode()
    {
        _currentTimeStayInBlackHole = 0;
        Destroy(_currentBlackHole);
        _currentBlackHole = null;
        isInBlackHole = false;
    }

    public void ShootAsteroid(InputAction.CallbackContext input)
    {
        if (input.phase == InputActionPhase.Performed && !isInBlackHole)
        {
            _playerShoot.ShootAsteroid();
        }
    }

    public float GetSpaceShipMaxSpeed()
    {
        return _maxSpeed;
    }

    public void SetLockOnIcon(bool isLockOn)
    {
        _beLockedOnIcon.SetActive(isLockOn);
    }

    public void SetIfInPlanet(bool isInPlanet, Transform planetTran=null)
    {
        _canInteractWithPlanet = isInPlanet;
        if (isInPlanet)
        {
            _currentPlanet = planetTran.gameObject;
        }
    }
}
