using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedusaHealthScript : EnemyHealthScript
{
    public override void Damage(int damage)
    {
        int childNum = transform.Find("Gorgons").childCount;
        if (childNum<=0)
        {
            base.Damage(damage);
        }
    }
}
