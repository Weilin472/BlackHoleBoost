using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [11/20/24]
 * [starts a timer to return to pool after some time]
 */

public class PickupSmallAsteroidTimer : MonoBehaviour
{
    private PickupSmallAsteroid _pickupSmallAsteroid;

    [SerializeField] private float _lifeTime;

    private void OnEnable()
    {
        _pickupSmallAsteroid = GetComponent<PickupSmallAsteroid>();
        StartCoroutine(AutoDestroy());
    }

    private IEnumerator AutoDestroy()
    {
        yield return new WaitForSeconds(_lifeTime);
        _pickupSmallAsteroid.ReturnToPool();
    }
}
