using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHealthscript : EnemyHealthScript
{
    public override void Damage(int damage)
    {
        base.Damage(damage);
        Debug.Log("child get hurt");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("child gets hurt");
    }
}
