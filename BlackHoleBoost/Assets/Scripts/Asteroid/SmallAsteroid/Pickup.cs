using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [09/11/2024]
 * [pick up for when player flies over item]
 */

public class Pickup : MonoBehaviour
{
    [SerializeField] private SmallAsteroidType _type = SmallAsteroidType.NORMAL;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.gameObject.GetComponent<PlayerAsteroidInventory>())
        {
            
        }
    }
}
