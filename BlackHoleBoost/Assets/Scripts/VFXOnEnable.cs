using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [11/18/2024]
 * [on enable, play the vfx]
 */

public class VFXOnEnable : MonoBehaviour
{
    [SerializeField] private ParticleSystem _VFX;

    private void OnEnable()
    {
        _VFX.Play();
    }
}
