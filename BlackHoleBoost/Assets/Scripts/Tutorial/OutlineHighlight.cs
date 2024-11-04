using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [11/03/2024]
 * [outlines highlighted target]
 */

public class OutlineHighlight : BaseHighlight
{
    [SerializeField] private MeshRenderer[] _renderers;
    [SerializeField] private Material _highlightMat;

    /// <summary>
    /// outlines object with shader 
    /// </summary>
    public override void Highlight()
    {
        foreach (MeshRenderer renderer in _renderers)
        {
            Material[] newMats = renderer.materials;
            System.Array.Resize(ref newMats, newMats.Length + 1);
            newMats[newMats.Length - 1] = _highlightMat;
            renderer.materials = newMats;
        }
    }

    /// <summary>
    /// removes outlines
    /// </summary>
    public override void Dehighlight()
    {
        foreach (MeshRenderer renderer in _renderers)
        {
            Material[] newMats = renderer.materials;
            System.Array.Resize(ref newMats, newMats.Length - 1);
            renderer.materials = newMats;
        }
    }
}
