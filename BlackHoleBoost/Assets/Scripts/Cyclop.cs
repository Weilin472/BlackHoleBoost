using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cyclop : MonoBehaviour
{
    [SerializeField]private Transform _laser;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private int _maxLife;

    private Rigidbody _rigid;
    private int _currentLife;

    private void Start()
    {
        _rigid = transform.GetComponent<Rigidbody>();
        _currentLife = _maxLife;
    }


    private void FixedUpdate()
    {
        if (!PlayerControl.Instance.isInBlackHole)
        {
            Vector3 playerPos = PlayerControl.Instance.transform.position;
            Vector3 dir = (playerPos - transform.position).normalized;
            _rigid.velocity = dir * _speed;
        }  
    }

    private void Update()
    {
        transform.Rotate(0, 0, -_rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            PlayerControl.Instance.Hurt(1);
        }
        if (other.tag=="BlackHole")
        {
            Hurt(1);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "BlackHole")
        {
            Hurt(1);
        }
    }

    private void Hurt(int damage)
    {
        _currentLife -= damage;
        Debug.Log(_currentLife);
        if (_currentLife<=0)
        {
            Destroy(gameObject);
        }
    }
}
