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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            Debug.Log("buh");
            PlayerHealthScript otherHealth = other.transform.root.gameObject.GetComponent<PlayerHealthScript>();
            otherHealth.Damage(1);
        }
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
