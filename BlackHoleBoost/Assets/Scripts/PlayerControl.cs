using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Rigidbody rigid;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    public void Move(InputAction.CallbackContext input)
    {
        rigid.velocity = new Vector3(input.ReadValue<Vector2>().x, input.ReadValue<Vector2>().y, 0)*_speed;

    }
}
