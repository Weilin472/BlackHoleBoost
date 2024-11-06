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
    [SerializeField] private float _minimumSpeed;
    [SerializeField] private float _maxBlackHoleSpeed;
    [SerializeField] private GameObject _beLockedOnIcon;
    [SerializeField] private float _blackHoleSuckUpMaxTime;

    private PlayerInput _playerInput;
    public bool IsFreeze;

    private GameObject _currentBlackHole;
    private GameObject _currentPlanet;
    private float _currentBlachHoleSpeed;
    [SerializeField] private float _constantBlackHoleSpeed;
    private float _circleModeRotateDiameter;
    private Rigidbody rigid;
    private float _currentBlackHoleModeRotateSpeed;
    private float _currentTimeStayInBlackHole;
    private int _currentBlackHolePhase = 0;

    private PlayerShoot _playerShoot;

    public bool isInBlackHole;
    private bool _canInteractWithPlanet;
    private bool _isInPlanet;
    private bool _isClockDirection;

    private bool _tutorial = false;
    private bool _tutorialAcceleration = false;
    private bool _tutorialStrafing = false;
    private bool _tutorialBlackhole = false;
    private bool _tutorialShooting = false;


    private bool _isMovingLeft;
    private bool _isMovingRight;
    private bool _isAccelerating;
    private bool _isSlowingDown;
    private bool canMove;
    private bool canShoot;

    //variables for controls and aiming
    private bool _isGamepad;
    [SerializeField] private float _controllerDeadzone = 0.1f;
    [SerializeField] private float _gamepadRotateSmoothing = 1000f;
    [SerializeField] private GameObject _aimDirection;
    private Vector2 _aim;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        _playerShoot = GetComponent<PlayerShoot>();
        _playerInput = GetComponent<PlayerInput>();
        if (!Mouse.current.enabled)
        {
            InputSystem.EnableDevice(Mouse.current);
        }
        canMove = true;
        canShoot = true;
        Time.timeScale = 1;
    }

    private void Update()
    {
        HandleAimRotation();


        if (isInBlackHole||_isInPlanet)
        {
            if (_currentBlachHoleSpeed<_maxBlackHoleSpeed)
            {
                _currentBlachHoleSpeed +=  Time.deltaTime;
              
            }
            _currentBlackHoleModeRotateSpeed =360/(Mathf.PI * _circleModeRotateDiameter / _constantBlackHoleSpeed);

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
                if (_currentTimeStayInBlackHole >= _blackHoleSuckUpMaxTime)
                {

                    isInBlackHole = false;
                    transform.GetComponent<PlayerHealthScript>().Damage(99999);
                    ExitBlackHoleMode();
                    rigid.velocity = Vector3.zero;
                }
                else
                {
                    int checkPhase = (int)(_currentTimeStayInBlackHole / (_blackHoleSuckUpMaxTime / 5));
                    if (checkPhase != _currentBlackHolePhase)
                    {
                        _currentBlackHolePhase = checkPhase;
                        _currentBlackHole.GetComponent<BlackHoleTextureManager>().SwapPhase(_currentBlackHolePhase);
                    }
                }
            }      
            return;
        }
    }

    private void FixedUpdate()
    {
        if (isInBlackHole||_isInPlanet)
        {
            rigid.velocity = transform.TransformDirection(Vector3.up)* _constantBlackHoleSpeed;
            
            return;
        }
        if (!isInBlackHole&&rigid.velocity.magnitude>_maxSpeed)
        {
            rigid.AddForce(transform.TransformDirection(Vector3.down) * _accelerationMultipler, ForceMode.Acceleration);
        }
        if (canMove)
        {
            Movement();
        }

    }

    private void Movement()
    {
        if (_isAccelerating)
        {
            if (Mathf.Abs(rigid.velocity.magnitude) < _maxSpeed)
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

            if (Mathf.Abs(rigid.velocity.magnitude) > 0)
            {
                rigid.AddForce(transform.TransformDirection(Vector3.down) * _accelerationMultipler, ForceMode.Acceleration);
                if (rigid.velocity.magnitude < _minimumSpeed)
                {
                    Vector3 relativeVelocity = transform.InverseTransformDirection(rigid.velocity);
                    relativeVelocity.y = _minimumSpeed;
                    rigid.velocity = transform.TransformDirection(relativeVelocity);
                }
            }
        }

        if (_isMovingLeft)
        {
            Vector3 relativeVelocity = transform.InverseTransformDirection(rigid.velocity);
            relativeVelocity.x = -_sideMoveSpeed;
            if (!IsNormalStrafing())
            {
                Debug.Log("Turned off switch strafing for playtest");
                //relativeVelocity.x *= -1;
            }
            rigid.velocity = transform.TransformDirection(relativeVelocity);
        }
        else if (_isMovingRight)
        {
            Vector3 relativeVelocity = transform.InverseTransformDirection(rigid.velocity);
            relativeVelocity.x = _sideMoveSpeed;
            if (!IsNormalStrafing())
            {
                Debug.Log("Turned off switch strafing for playtest");
                //relativeVelocity.x *= -1;
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
        if (!_tutorial || _tutorial && _tutorialStrafing)
        {
            if (input.phase == InputActionPhase.Performed)
            {
                _isMovingLeft = true;
            }
            else if (input.phase == InputActionPhase.Canceled)
            {
                _isMovingLeft = false;
                //   rigid.velocity = new Vector3(0, rigid.velocity.y, rigid.velocity.z);
                Vector3 relativeVelocity = transform.InverseTransformDirection(rigid.velocity);
                relativeVelocity.x = 0;
                rigid.velocity = transform.TransformDirection(relativeVelocity);
            }
        }
        
    }

    public void RightMove(InputAction.CallbackContext input)
    {
        if (!_tutorial || _tutorial && _tutorialStrafing)
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
    }

    public void Acceleration(InputAction.CallbackContext input)
    {
        if (!_tutorial || _tutorial && _tutorialAcceleration)
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
    }

    public void SlowDown(InputAction.CallbackContext input)
    {
        if (!_tutorial || _tutorial && _tutorialAcceleration)
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
    }
    
    public void SpawnBlackHole(InputAction.CallbackContext input)
    {
        if (!_tutorial || _tutorial && _tutorialBlackhole)
        {
            if (input.phase == InputActionPhase.Performed && !IsFreeze)
            {
                if (!isInBlackHole && !_canInteractWithPlanet && !_isInPlanet)//spawn black holes
                {
                    if ((_isMovingLeft && IsNormalStrafing()) || (_isMovingRight && !IsNormalStrafing()) || (!_isMovingLeft && !_isMovingRight && !IsNormalStrafing()))
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
                    _currentBlachHoleSpeed = _constantBlackHoleSpeed;
                    _circleModeRotateDiameter = _currentBlackHole.transform.localScale.x * 0.8f;
                    Collider[] colliders = Physics.OverlapSphere(_currentBlackHole.transform.position, _currentBlackHole.transform.localScale.x / 2);
                    for (int i = 0; i < colliders.Length; i++)
                    {
                        if (colliders[i].tag == "Planet")
                        {
                            Destroy(colliders[i].gameObject);
                        }
                    }
                }
                else if (_canInteractWithPlanet && !_isInPlanet && !isInBlackHole)//circle the planets
                {
                    _isInPlanet = true;
                    _canInteractWithPlanet = false;
                    _currentBlachHoleSpeed = _constantBlackHoleSpeed;
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
                else if (isInBlackHole)//destory blackhole
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
    }

    public void ExitBlackHoleMode()
    {
        _currentTimeStayInBlackHole = 0;
        Destroy(_currentBlackHole);
        _currentBlackHole = null;
        isInBlackHole = false;
        Vector3 tempSpeed = transform.InverseTransformDirection(rigid.velocity);
        tempSpeed.y = _currentBlachHoleSpeed;
        rigid.velocity = transform.TransformDirection(tempSpeed);
    }

    /// <summary>
    /// calls to shoot asteroid
    /// </summary>
    /// <param name="input"></param>
    public void ShootAsteroid(InputAction.CallbackContext input)
    {
        if (!_tutorial || _tutorial && _tutorialShooting)
        {
            if (input.phase == InputActionPhase.Performed && canShoot)
            {
                _playerShoot.ShootAsteroid();
            }
        }
    }

    /// <summary>
    /// calls to switch asteroid
    /// </summary>
    /// <param name="input"></param>
    public void SwitchAsteroid(InputAction.CallbackContext input)
    {
        if (!_tutorial || _tutorial && _tutorialShooting)
        {
            if (input.phase == InputActionPhase.Performed)
            {
                _playerShoot.SwitchCurrentAsteroid();
            }
        }
    }
    
    //gets input for aiming
    public void AimAsteroid(InputAction.CallbackContext input)
    {
        _aim = input.ReadValue<Vector2>();
    }

    /// <summary>
    /// makes sure the game knows which input to take
    /// temp: make sure to lock controls before round starts
    /// </summary>
    /// <param name="pi"></param>
    public void OnDeviceChange(PlayerInput pi)
    {
        _isGamepad = pi.currentControlScheme.Equals("Gamepad") ? true : false;
    }

    /// <summary>
    /// handles rotation of aiming
    /// </summary>
    private void HandleAimRotation()
    {
        if (_isGamepad)
        {
            if (Mathf.Abs(_aim.x) > _controllerDeadzone || Mathf.Abs(_aim.y) > _controllerDeadzone)
            {
                Vector3 playerDirection = Vector3.right * _aim.x + Vector3.up * _aim.y;
                if (playerDirection.sqrMagnitude > 0.0f)
                {
                    Quaternion newRotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg - 90f);
                    _aimDirection.transform.rotation = Quaternion.RotateTowards(_aimDirection.transform.rotation, newRotation, _gamepadRotateSmoothing * Time.deltaTime);
                }
            }
            else
            {
                Vector3 playerDirection = transform.up;
                Quaternion newRotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg - 90f);
                _aimDirection.transform.rotation = Quaternion.RotateTowards(_aimDirection.transform.rotation, newRotation, _gamepadRotateSmoothing * Time.deltaTime);
            }
        }
        else
        {
            Ray ray = Camera.main.ScreenPointToRay(_aim);
            Plane groundPlane = new Plane(Vector3.back, Vector3.zero);
            float rayDistance;

            if (groundPlane.Raycast(ray, out rayDistance))
            {
                Vector3 point = ray.GetPoint(rayDistance);
                Vector3 playerDirection = new Vector3(point.x, point.y, 0);
                playerDirection = playerDirection - transform.position;
                if (playerDirection.sqrMagnitude > 0.0f)
                {
                    Quaternion newRotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg - 90f);
                    _aimDirection.transform.rotation = Quaternion.RotateTowards(_aimDirection.transform.rotation, newRotation, _gamepadRotateSmoothing * Time.deltaTime);
                }
            }
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

    public void Freeze()
    {
        IsFreeze = true;
        canMove = false;
        rigid.velocity = Vector3.zero;
        Invoke("UnFreeze", 2.5f);   
    }
    void UnFreeze()
    {
        IsFreeze = false;
        canMove = true;
    }

    public void HitBySphinx(int damage,float effectTime)
    {
        transform.GetComponent<PlayerHealthScript>().Damage(damage);
        canShoot = false;
        CancelInvoke("ResetFromSphinx");
        Invoke("ResetFromSphinx", effectTime);
    }

    private void ResetFromSphinx()
    {
        canShoot = true;
    }

    /// <summary>
    /// flags to lock controls for tutorial
    /// </summary>
    public void TutorialControls()
    {
        _tutorial = true;
    }

    /// <summary>
    /// makes sure tutorial controls is off
    /// </summary>
    public void EndTutorial()
    {
        _tutorial = false;
    }

    /// <summary>
    /// flags to unlock acceleration controls for tutorial
    /// </summary>
    public void UnlockTutorialAcceleration()
    {
        _tutorialAcceleration = true;
    }

    /// <summary>
    /// flags to unlock strafing controls for tutorial
    /// </summary>
    public void UnlockTutorialStrafing()
    {
        _tutorialStrafing = true;
    }

    /// <summary>
    /// flags to unlock Blackhole controls for tutorial
    /// </summary>
    public void UnlockTutorialBlackhole()
    {
        _tutorialBlackhole = true;
    }

    /// <summary>
    /// flags to unlock shooting controls for tutorial
    /// </summary>
    public void UnlockTutorialShooting()
    {
        _tutorialShooting = true;
    }
}
