using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JustinTestScript : MonoBehaviour
{
    private EnemyAsteroidPool _pool;

    private void Start()
    {
        _pool = GetComponent<EnemyAsteroidPool>();
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Spawn Asteroid"))
        {
            _pool.Spawn();
        }
    }
}
