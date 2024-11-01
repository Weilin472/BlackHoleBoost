using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [11/01/2024]
 * [Sends models for stickyAsteroid]
 */

public class EnemyAsteroidSticky : MonoBehaviour
{
    private EnemyAsteroidModel _enemyAsteroidModel;

    [SerializeField] private GameObject _bigModel;
    [SerializeField] private GameObject _mediumModel;

    /// <summary>
    /// get needed components
    /// </summary>
    private void OnEnable()
    {
        _enemyAsteroidModel = GetComponent<EnemyAsteroidModel>();
    }

    /// <summary>
    /// returns the correct model for a sticky asteroid
    /// </summary>
    /// <returns>model for sticky</returns>
    public GameObject GetModel()
    {
        if (_enemyAsteroidModel.currentBig)
        {
            return ModelSetup(_bigModel);
        }
        else
        {
            return ModelSetup(_mediumModel);
        }
    }

    /// <summary>
    /// returns model after modified for sticky asteroid
    /// </summary>
    /// <param name="model">medium or big asteroid</param>
    /// <returns>modified model</returns>
    private GameObject ModelSetup(GameObject model)
    {
        GameObject modifiedModel = model;
        modifiedModel.GetComponent<SphereCollider>().enabled = true;
        return modifiedModel;
    }
}
