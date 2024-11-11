using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [11/10/2024]
 * [Health for the body]
 */

public class CerberusHealth : EnemyHealthScript
{
    [SerializeField] private GameObject[] _cerberusHeads = new GameObject[3];

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
