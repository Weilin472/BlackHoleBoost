using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedusaDamage : EnemyHealthScript
{
    public override void Damage(int damage)
    {
        Medusa medusa = transform.GetComponent<Medusa>();
        GameObject gorgon =medusa.GetNearestGorgon(medusa.targetPlayer.gameObject);
        if (gorgon!=null)
        {
            gorgon.transform.GetComponent<EnemyHealthScript>().Damage(damage);
        }
        else
        {
            base.Damage(damage);
        }
    }
}
