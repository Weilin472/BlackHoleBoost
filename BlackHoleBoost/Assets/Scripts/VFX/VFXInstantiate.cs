using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [12/06/2024]
 * [instantiates the vfx]
 */

public class VFXInstantiate : MonoBehaviour
{
    [SerializeField] private GameObject _vfx;

    /// <summary>
    /// instantiates the vfx
    /// </summary>
    /// <param name="loc">where to make the vfx</param>
    public void SpawnVFX(Vector3 loc)
    {
        Instantiate(_vfx, loc, Quaternion.identity);
    }
}
