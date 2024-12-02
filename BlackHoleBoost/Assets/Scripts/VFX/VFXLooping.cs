using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [12/01/2024]
 * [scripting to start and stop looping vfx]
 */

public class VFXLooping : MonoBehaviour
{
    [SerializeField] private ParticleSystem _VFX;

    public void StartVFX()
    {
        _VFX.Play();
    }

    public void StopVFX()
    {
        _VFX.Stop();
    }
}
