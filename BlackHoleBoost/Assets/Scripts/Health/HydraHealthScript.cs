using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HydraHealthScript : EnemyHealthScript
{
    public override void Damage(int damage)
    {
        if (CheckAttackable())
        {
            HeadofHydraHealth[] heads = transform.GetComponentsInChildren<HeadofHydraHealth>();
            if (heads.Length>0)
            {
                Destroy(heads[0].gameObject);
                RegrownAllHead();
            }
            else
            {
                base.Damage(damage);
            }
        }

    }

    private bool CheckAttackable()
    {
        bool canAttack = true;
        HeadofHydraHealth[] heads = transform.GetComponentsInChildren<HeadofHydraHealth>();
        for (int i = 0; i < heads.Length; i++)
        {
            if (heads[i].hasHead)
            {
                return false;
            }
        }
        return canAttack;
    }

    private void RegrownAllHead()
    {
        HeadofHydraHealth[] heads = transform.GetComponentsInChildren<HeadofHydraHealth>();
        for (int i = 0; i < heads.Length; i++)
        {
            heads[i].RegrowHead();
        }
    }

    /// <summary>
    /// checks if the head killed is a freebie kill before heads start to regrow
    /// </summary>
    /// <param name="head"></param>
    public void CheckFreebieKill(HeadofHydraHealth head)
    {
        HeadofHydraHealth[] heads = transform.GetComponentsInChildren<HeadofHydraHealth>();
        if (heads.Length > 3)
        {
            Destroy(head.gameObject);
        }
    }
}
