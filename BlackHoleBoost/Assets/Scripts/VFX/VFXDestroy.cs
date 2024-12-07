using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [12/06/2024]
 * [destroys vfx after being played]
 */

public class VFXDestroy : MonoBehaviour
{
    private void OnEnable()
    {
        Destroy(gameObject, 3);
    }
}
