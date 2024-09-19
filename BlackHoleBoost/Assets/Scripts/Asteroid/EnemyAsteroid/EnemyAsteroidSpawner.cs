using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [9/17/2024]
 * [Any gameobject with this script, will wrap around the screen]
 */

public class EnemyAsteroidSpawner : MonoBehaviour
{
    private EnemyAsteroidPool _enemyAsteroidPool;

    private void Awake()
    {
        _enemyAsteroidPool = GetComponent<EnemyAsteroidPool>();
    }


}
