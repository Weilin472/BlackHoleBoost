using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [12/06/2024]
 * [Health for the body]
 */

public class CerberusHealth : EnemyHealthScript
{
    [SerializeField] private GameObject[] _cerberusHeads = new GameObject[3];

    /// <summary>
    /// if all of the heads are down, then the player can hurt the body
    /// </summary>
    /// <param name="damage"></param>
    public override void Damage(int damage)
    {
        foreach (GameObject head in _cerberusHeads)
        {
            if (head.activeSelf)
            {
                return;
            }
        }

        base.Damage(damage);
    }
}
