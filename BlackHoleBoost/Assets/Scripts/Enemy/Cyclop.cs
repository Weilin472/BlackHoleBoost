using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cyclop : EnemyBase
{
    [SerializeField]private Transform _laser;
    [SerializeField] private float _rotateSpeed;

    private void Update()
    {
        transform.Rotate(0, 0, -_rotateSpeed * Time.deltaTime);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.tag=="BlackHole")
        {
            _enemyHealthScript.Damage(1);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "BlackHole")
        {
            _enemyHealthScript.Damage(1);
        }
    }

  
}
