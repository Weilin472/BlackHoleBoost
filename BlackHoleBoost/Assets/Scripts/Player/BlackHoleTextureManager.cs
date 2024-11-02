using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [11/01/2024]
 * [script to update the blackhole textures]
 */

public class BlackHoleTextureManager : MonoBehaviour
{
    [SerializeField] private MeshRenderer _coreRenderer;
    [SerializeField] private MeshRenderer _innerRenderer;
    [SerializeField] private MeshRenderer _outerRenderer;
    [SerializeField] private MeshRenderer _ringRenderer;

    [SerializeField] private Material[] _coreMats;
    [SerializeField] private Material[] _innerMats;
    [SerializeField] private Material[] _outerMats;
    [SerializeField] private Material[] _ringMats;

    /// <summary>
    /// changes the black hole textures to match phase
    /// </summary>
    /// <param name="phase"></param>
    public void SwapPhase(int phase)
    {
        _coreRenderer.material = _coreMats[phase];
        _innerRenderer.material = _innerMats[phase];
        _outerRenderer.material = _outerMats[phase];
        _ringRenderer.material = _ringMats[phase];
    }
}
