using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [09/07/2024]
 * [sets damage for asteroid]
 */

public class EnemyAsteroidDamage : BaseDamageScript
{
    [SerializeField] private int _bigDamage = 3;
    [SerializeField] private int _mediumDamage = 1;

    /// <summary>
    /// sets health to big asteroid
    /// </summary>
    public void SetBig()
    {
        _damage = _bigDamage;
    }

    /// <summary>
    /// sets health to med asteroid
    /// </summary>
    public void SetMedium()
    {
        _damage = _mediumDamage;
    }
}
