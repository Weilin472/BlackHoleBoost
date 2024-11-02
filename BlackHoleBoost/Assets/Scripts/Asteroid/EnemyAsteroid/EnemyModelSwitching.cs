using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [10/31/2024]
 * [switches model depending on the dropped item]
 */

public class EnemyModelSwitching : MonoBehaviour
{
    [SerializeField] private GameObject[] _asteroidModels;
    [SerializeField] private MeshRenderer[] _renderers;

    [SerializeField] private Material _normalMat;
    [SerializeField] private Material _bounceMat;
    [SerializeField] private Material _stickyMat;

    /// <summary>
    /// switches model for asteroid
    /// </summary>
    /// <param name="asteroidType">type of asteroid</param>
    public void SwapModel(SmallAsteroidType asteroidType)
    {
        foreach (GameObject model in _asteroidModels)
        {
            model.SetActive(false);
        }

        int modelIndex = Random.Range(0, _asteroidModels.Length);
        _asteroidModels[modelIndex].SetActive(true);
        _asteroidModels[modelIndex].transform.rotation = Random.rotation;
        for (int i = 0; i < _renderers.Length; i++)
        {
            switch (asteroidType)
            {
                case SmallAsteroidType.NORMAL:
                    _renderers[i].material = _normalMat;
                    break;
                case SmallAsteroidType.BOUNCE:
                    _renderers[i].material = _bounceMat;
                    break;
                case SmallAsteroidType.STICKY:
                    _renderers[i].material = _stickyMat;
                    break;
                default:
                    break;
            }
        }
        
    }
}
