using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [12/06/2024]
 * [script for all of the indicators]
 */

public class PlayerEnemyIndicator : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemyIndicators;

    /// <summary>
    /// changes the indicator for the player
    /// index:
    /// 0 - minotaur
    /// 1 - madusa
    /// 2 - sphinx
    /// 
    /// </summary>
    /// <param name="index">index of enemy indicator</param>
    /// <param name="active">is the indicator set active</param>
    public void ChangeIndicator(int index, bool active)
    {
        _enemyIndicators[index].SetActive(active);
    }

    /// <summary>
    /// just sets all indicators to false
    /// </summary>
    public void ResetIndicator()
    {
        for (int i = 0; i < _enemyIndicators.Length; i++)
        {
            _enemyIndicators[i].SetActive(false);
        }
    }
}
