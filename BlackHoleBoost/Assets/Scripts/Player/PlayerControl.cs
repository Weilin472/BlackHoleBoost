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



    private GameObject _currentBlackHole;
    private float _currentBlachHoleSpeed;
    private Rigidbody rigid;
    private float _currentBlackHoleModeRotateSpeed;
    private static PlayerControl _instance;

    private PlayerShoot _playerShoot;

    public bool isInBlackHole;
    public static PlayerControl Instance => _instance;

    private void Awake()
    {
        if (_instance!=null )
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        _playerShoot = GetComponent<PlayerShoot>();
    }

    private void Update()
    {
      
        if (isInBlackHole)
        {            
            if (_currentBlachHoleSpeed<_maxBlackHoleSpeed)
            {
                _currentBlachHoleSpeed +=  Time.deltaTime;
            }
            _currentBlackHoleModeRotateSpeed =360/(Mathf.PI * _currentBlackHole.transform.localScale.x*0.8f / _currentBlachHoleSpeed);
            transform.Rotate(0, 0, -_currentBlackHoleModeRotateSpeed * Time.deltaTime);
            return;
        }

        if (Input.GetKeyUp(KeyCode.A)||Input.GetKeyUp(KeyCode.D))
        {
            rigid.velocity = new Vector3(0, rigid.velocity.y, rigid.velocity.z);
        }
    }

    private void FixedUpdate()
    {
        if (isInBlackHole)
        {
            rigid.velocity = transform.TransformDirection(Vector3.up)*_currentBlachHoleSpeed;
            
            return;
        }
        if (!isInBlackHole&&rigid.velocity.magnitude>_maxSpeed)
        {
            rigid.AddForce(transform.TransformDirection(Vector3.down) * _accelerationMultipler, ForceMode.Acceleration);
        }
        if (Input.GetKey(KeyCode.W))
        {
            if (Mathf.Abs(rigid.velocity.magnitude)<_maxSpeed)
            {
                rigid.AddForce(transform.TransformDirection(Vector3.up) * _accelerationMultipler, ForceMode.Acceleration);
            }
            else
            {
                rigid.velocity = transform.TransformDirection(Vector3.up) * _maxSpeed;
            }
        }
        else if (Input.GetKey(KeyCode.S))
        {

            if (Mathf.Abs(rigid.velocity.magnitude)>_accelerationMultipler)
            {
                rigid.AddForce(transform.TransformDirection(Vector3.down) * _accelerationMultipler, ForceMode.Acceleration);
            }
            else
            {
                rigid.velocity = new Vector3(rigid.velocity.x, 0, rigid.velocity.z);
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            rigid.velocity =new Vector3(-_sideMoveSpeed, rigid.velocity.y, rigid.velocity.z);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rigid.velocity =new Vector3(_sideMoveSpeed, rigid.velocity.y, rigid.velocity.z);
        }
    }
    
    public void SpawnBlackHole(InputAction.CallbackContext input)
    {
        if (input.phase == InputActionPhase.Performed)
        {
            if (!isInBlackHole)
            {
                _currentBlackHole = Instantiate(_blackHolePrefab, transform.position + transform.TransformDirection(Vector3.right)*0.8f, Quaternion.identity);
                isInBlackHole = true;
                _currentBlachHoleSpeed =Mathf.Abs(rigid.velocity.magnitude);
            }
            else
            {
                Destroy(_currentBlackHole);
                _currentBlackHole = null;
                isInBlackHole = false;
            }         
        }
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

}
