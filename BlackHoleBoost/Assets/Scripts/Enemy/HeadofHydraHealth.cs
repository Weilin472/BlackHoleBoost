using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadofHydraHealth : EnemyHealthScript
{
    public bool hasHead = true;
    [SerializeField] private GameObject head;
    [SerializeField] private float _regrowHeadTime;
    public override void Damage(int damage)
    {
        if (hasHead)
        {
            hasHead = false;
            head.gameObject.SetActive(false);
            Invoke("RegrowHead", _regrowHeadTime);
        }
    }

    public void RegrowHead()
    {
        hasHead = true;
        head.gameObject.SetActive(true);
        CancelInvoke();
    }

    private void OnDestroy()
    {
        CancelInvoke();
    }
}
