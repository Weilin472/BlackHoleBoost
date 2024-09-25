using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPickUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            other.transform.GetComponent<PlayerHealthScript>().Heal(1);
            Destroy(gameObject);
        }
    }
}
