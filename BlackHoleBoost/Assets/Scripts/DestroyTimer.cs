using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [11/20/24]
 * [starts a timer to destroy game object]
 */

public class DestroyTimer : MonoBehaviour
{
    [SerializeField] private float _lifeTime;

    private void OnEnable()
    {
        Destroy(gameObject, _lifeTime);
    }
}
