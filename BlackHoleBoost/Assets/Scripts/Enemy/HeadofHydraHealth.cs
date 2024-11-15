using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadofHydraHealth : EnemyHealthScript
{
    private HydraHealthScript _hydraHealthScript;
    public bool hasHead = true;
    [SerializeField] private GameObject head;
    [SerializeField] private float _regrowHeadTime;

    private void OnEnable()
    {
        _hydraHealthScript = transform.root.gameObject.GetComponent<HydraHealthScript>();
    }

    public override void Damage(int damage)
    {
        if (hasHead)
        {
            _hydraHealthScript.CheckFreebieKill(this);
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
