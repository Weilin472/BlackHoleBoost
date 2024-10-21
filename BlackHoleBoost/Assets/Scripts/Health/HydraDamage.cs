using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HydraDamage : EnemyHealthScript
{
    public override void Damage(int damage)
    {
        base.Damage(damage);
        Debug.Log("parent get hurt");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("something get hit");
    }
}
