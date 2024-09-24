using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthScript : BaseHealthScript
{
    [SerializeField] private float _UnattackablePeriod;

    private bool _isUnattackable = false;

    private void Awake()
    {
        ResetHealth();
    }

    public override void Damage(int damage)
    {
        if (!PlayerControl.Instance.isInBlackHole && !_isUnattackable)
        {
            StartCoroutine(HurtAnimation());
            base.Damage(damage);
            UIManager.Instance.SetLifeUI(_currentHealth);
        }
    }

    protected override void OnDeath()
    {
        //Time.timeScale = 0;
        Debug.Log("prototype game over here, remove later");
        PrototypeGameManager.Instance.GameOver();
        Destroy(gameObject);
    }

    private IEnumerator HurtAnimation()
    {
        _isUnattackable = true;
        float _currentUnattackTime = 0;
        MeshRenderer[] mr = transform.GetComponentsInChildren<MeshRenderer>();
        while (_currentUnattackTime < _UnattackablePeriod)
        {
            for (int i = 0; i < mr.Length; i++)
            {
                mr[i].enabled = !mr[i].enabled;
            }
            _currentUnattackTime += 0.5f;
            yield return new WaitForSeconds(0.5f);
        }
        for (int i = 0; i < mr.Length; i++)
        {
            mr[i].enabled = true;
        }
        _isUnattackable = false;
    }
}
