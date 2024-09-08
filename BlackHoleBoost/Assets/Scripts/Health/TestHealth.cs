using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHealth : BaseHealthScript
{
    protected override void OnDeath()
    {
        Destroy(gameObject);
    }
}
