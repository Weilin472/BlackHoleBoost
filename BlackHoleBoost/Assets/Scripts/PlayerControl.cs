using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float _originalMovementSpeed;
    private float _curretMovementSpeed;
    [SerializeField] private float _rotationSpeed;
    private Rigidbody rigid;

    [SerializeField] private GameObject _blackHolePrefab;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        _curretMovementSpeed = _originalMovementSpeed;
    }

    private void Update()
    {

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(new Vector3(0, 0, 15) * Time.deltaTime*_rotationSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(new Vector3(0, 0, -15) * Time.deltaTime*_rotationSpeed);
        }

        //if (Input.GetKey(KeyCode.LeftShift))
        //{
        //    _curretMovementSpeed = _originalMovementSpeed*2;
        //}
        //else if (Input.GetKeyUp(KeyCode.LeftShift))
        //{
        //    _curretMovementSpeed = _originalMovementSpeed;
        //}
        
    }

    private void FixedUpdate()
    {
        rigid.velocity = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            //   rigid.AddRelativeForce(Vector3.up*_movementSpeed, ForceMode.Acceleration);
            rigid.velocity =transform.TransformDirection(Vector3.up) * _curretMovementSpeed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            //  rigid.AddRelativeForce(Vector3.down* _movementSpeed, ForceMode.Acceleration);
            rigid.velocity = transform.TransformDirection(Vector3.down) * _curretMovementSpeed;
        }
    }

    public void Acceleration(InputAction.CallbackContext input)
    {
        if (input.phase==InputActionPhase.Performed)
        {
            _curretMovementSpeed = _originalMovementSpeed * 2;

        }
        else if (input.phase==InputActionPhase.Canceled)
        {
            _curretMovementSpeed = _originalMovementSpeed;
        }
    }

    
    public void SpawnBlackHole(InputAction.CallbackContext input)
    {
        if (input.phase == InputActionPhase.Performed)
        {
            GameObject.Instantiate(_blackHolePrefab, (transform.position+ transform.TransformDirection(Vector3.up)) * 2, Quaternion.identity);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag=="BlackHole")
        {
            Vector3 blackholeForce = (other.transform.position - transform.position)*20;
            rigid.AddForce(blackholeForce,ForceMode.Force);
        }
    }



}
