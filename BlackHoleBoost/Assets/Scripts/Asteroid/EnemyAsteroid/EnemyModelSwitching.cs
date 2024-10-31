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

    [SerializeField] private Material _normalMat;
    [SerializeField] private Material _bounceMat;
    [SerializeField] private Material _stickyMat;

    /// <summary>
    /// switches model for asteroid
    /// </summary>
    /// <param name="type">type of asteroid</param>
    public void SwitchModel(SmallAsteroidType type)
    {
        foreach (GameObject model in _asteroidModels)
        {
            model.SetActive(false);
        }

        int modelIndex = Random.Range(0, _asteroidModels.Length);
        _asteroidModels[modelIndex].SetActive(true);
        MeshRenderer[] renderer = _asteroidModels[modelIndex].GetComponentsInChildren<MeshRenderer>();
        for (int i = 0; i < renderer.Length; i++)
        {
            switch (type)
            {
                case SmallAsteroidType.NORMAL:
                    renderer[i].material = _normalMat;
                    break;
                case SmallAsteroidType.BOUNCE:
                    renderer[i].material = _bounceMat;
                    break;
                case SmallAsteroidType.STICKY:
                    renderer[i].material = _stickyMat;
                    break;
                default:
                    break;
            }
        }
        
    }
}
