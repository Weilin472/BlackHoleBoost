using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [11/03/2024]
 * [Base Class for highlighting something in tutorial]
 */

public class TutorialHighlightObject : MonoBehaviour
{
    [SerializeField] private MeshRenderer[] _renderers;
    [SerializeField] private Material _highlightMat;

    //
    public void Highlight()
    {
        foreach (MeshRenderer renderer in _renderers)
        {
            Material[] newMats = renderer.materials;
            System.Array.Resize(ref newMats, newMats.Length + 1);
            newMats[newMats.Length - 1] = _highlightMat;
            renderer.materials = newMats;
        }
    }

    public void Dehighlight()
    {
        foreach (MeshRenderer renderer in _renderers)
        {
            Material[] newMats = renderer.materials;
            System.Array.Resize(ref newMats, newMats.Length - 1);
            renderer.materials = newMats;
        }
    }
}
