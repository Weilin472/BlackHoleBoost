using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [10/31/24]
 * [sets the type of asteroid]
 */

public class EnemyAsteroidType : MonoBehaviour
{
    private EnemyAsteroid _enemyAsteroid;

    private void Awake()
    {
        _enemyAsteroid = GetComponent<EnemyAsteroid>();
    }

    private void OnEnable()
    {
        float dropSize = Random.Range(0f, 1f);
        if (dropSize <= .33f)
        {
            _enemyAsteroid.dropType = SmallAsteroidType.NORMAL;
        }
        else if (dropSize > .33f && dropSize <= .66f)
        {
            _enemyAsteroid.dropType = SmallAsteroidType.BOUNCE;
        }
        else
        {
            _enemyAsteroid.dropType = SmallAsteroidType.STICKY;
        }
    }
}
