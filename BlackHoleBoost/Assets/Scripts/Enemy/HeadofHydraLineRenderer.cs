using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [11/01/2024]
 * [script to update line renderers for hydra]
 */

public class HeadofHydraLineRenderer : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private GameObject _headModel;
    [SerializeField] private GameObject _bodyModel;

    /// <summary>
    /// every frame, updates hydra's neck to fallow
    /// </summary>
    private void Update()
    {
        _lineRenderer.SetPosition(0, _bodyModel.transform.position);
        _lineRenderer.SetPosition(1, _headModel.transform.position);
    }
}
