using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [09/07/2024]
 * [script to set model]
 */

public class EnemyAsteroidModel : MonoBehaviour
{
    [SerializeField] private GameObject _bigModel;
    [SerializeField] private GameObject _mediumModel;

    private void OnDisable()
    {
        _bigModel.SetActive(false);
        _mediumModel.SetActive(false);
    }

    /// <summary>
    /// sets model to big asteroid
    /// </summary>
    public void SetBig()
    {
        _bigModel.SetActive(true);
    }

    /// <summary>
    /// sets model to med asteroid
    /// </summary>
    public void SetMedium()
    {
        _mediumModel.SetActive(true);
    }
}
