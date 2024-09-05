using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _rotationSpeed;
    private Rigidbody rigid;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
      
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
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rigid.AddRelativeForce(Vector3.up*_movementSpeed, ForceMode.Acceleration);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rigid.AddRelativeForce(Vector3.down* _movementSpeed, ForceMode.Acceleration);
        }
    }

    

   
}
