using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [12/02/2024]
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
}
