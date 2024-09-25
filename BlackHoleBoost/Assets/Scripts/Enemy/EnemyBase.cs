using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] protected float _speed;
    protected Rigidbody _rigid;

    protected EnemyHealthScript _enemyHealthScript;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        _rigid = transform.GetComponent<Rigidbody>();
        _enemyHealthScript = GetComponent<EnemyHealthScript>();
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
        if (PlayerControl.Instance != null)
        {
            Vector3 playerPos = PlayerControl.Instance.transform.position;
            Vector3 dir = (playerPos - transform.position).normalized;
            _rigid.velocity = dir * _speed;
        }
    }
}
