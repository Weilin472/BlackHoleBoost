using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthScript : BaseHealthScript
{
    private void Awake()
    {
        ResetHealth();
    }

    protected override void OnDeath()
    {
        Destroy(gameObject);
    }
}
