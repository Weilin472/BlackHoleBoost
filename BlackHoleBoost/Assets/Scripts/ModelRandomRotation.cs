using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [11/20/24]
 * [Sets a random Rotation for model]
 */

public class ModelRandomRotation : MonoBehaviour
{
    private void OnEnable()
    {
        transform.rotation = Random.rotation;
    }
}
