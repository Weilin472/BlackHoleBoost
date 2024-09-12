using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float _originalMovementSpeed;
    private float _curretMovementSpeed;
    [SerializeField] private float _sideMoveSpeed;
    private Rigidbody rigid;
    [SerializeField] private GameObject _blackHolePrefab;
    private bool isInBlackHole;
    [SerializeField] private float _maxSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        _curretMovementSpeed = _originalMovementSpeed;
    }

    private void Update()
    {
      
        if (isInBlackHole)
        {
            return;
        }

        if (Input.GetKeyUp(KeyCode.A)||Input.GetKeyUp(KeyCode.D))
        {
            rigid.velocity = new Vector3(0, rigid.velocity.y, rigid.velocity.z);
        }

       
        Debug.Log(rigid.velocity);
    }

    private void FixedUpdate()
    {
        if (isInBlackHole)
        {
            return;
        }
        if (Input.GetKey(KeyCode.W))
        {
            if (Mathf.Abs(rigid.velocity.y)<_maxSpeed)
            {
                rigid.AddForce(transform.TransformDirection(Vector3.up) * _curretMovementSpeed, ForceMode.Acceleration);
            }               
        }
        else if (Input.GetKey(KeyCode.S))
        {

            if (Mathf.Abs(rigid.velocity.y)>_curretMovementSpeed)
            {
                rigid.AddForce(transform.TransformDirection(Vector3.down) * _curretMovementSpeed, ForceMode.Acceleration);
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
            GameObject.Instantiate(_blackHolePrefab, transform.position, Quaternion.identity);
            rigid.velocity = Vector3.zero;
            isInBlackHole = true;
        }
    }



}
