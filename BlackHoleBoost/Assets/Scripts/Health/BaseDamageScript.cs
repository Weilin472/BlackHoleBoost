using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDamageScript : MonoBehaviour
{
    [SerializeField] protected int _damage = 1;

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.gameObject.GetComponent<BaseHealthScript>())
        {
            BaseHealthScript otherHealth = other.transform.root.gameObject.GetComponent<BaseHealthScript>();
            otherHealth.GetHurt(_damage);
        }

    }
}
