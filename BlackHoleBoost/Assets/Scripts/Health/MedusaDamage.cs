using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedusaDamage : EnemyHealthScript
{
    public override void Damage(int damage)
    {
        int childNum = transform.Find("Gorgons").childCount;
        Debug.Log("shoot medusa");
        if (childNum<=0)
        {
            base.Damage(damage);
        }
    }
}
