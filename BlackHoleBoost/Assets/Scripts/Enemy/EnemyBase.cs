using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] protected float _speed;
    [SerializeField] protected int _maxLife;
    protected int _currentLife;
    protected Rigidbody _rigid;




    // Start is called before the first frame update
    protected virtual void Start()
    {
        _currentLife = _maxLife;
        _rigid = transform.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void FixedUpdate()
    {
        if (!PlayerControl.Instance.isInBlackHole)
        {
            Movement();
        }
    }

    protected virtual void Movement()
    {
       
            Vector3 playerPos = PlayerControl.Instance.transform.position;
            Vector3 dir = (playerPos - transform.position).normalized;
            _rigid.velocity = dir * _speed;
        
    }

    protected void Hurt(int damage)
    {
        _currentLife -= damage;
        Debug.Log(_currentLife);
        if (_currentLife <= 0)
        {
            Destroy(gameObject);
        }
    }
}
