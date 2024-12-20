using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [11/20/2024]
 * [health script for prototype]
 */

public class PlayerHealthScript : BaseHealthScript
{
    private PlayerControl _playerControl;

    [SerializeField] private float _UnattackablePeriod;

    private bool _isUnattackable = false;

    private void Awake()
    {
        _playerControl = GetComponent<PlayerControl>();
        ResetHealth();
    }

    private void Start()
    {
        UIManager.Instance.SetLifeUI(_maxHealth);
    }

    /// <summary>
    /// input a positive number to decrease its health
    /// </summary>
    /// <param name="damage"></param>
    public override void Damage(int damage)
    {
        if ( !_isUnattackable && !transform.GetComponent<PlayerControl>().isInBlackHole)
        {
            StartCoroutine(HurtAnimation());
            base.Damage(damage);
            UIManager.Instance.SetLifeUI(_currentHealth);
        }
    }

    /// <summary>
    /// damage even in shield
    /// </summary>
    /// <param name="damage"></param>
    public void BlackHoleDamage(int damage)
    {
        if (!_isUnattackable)
        {
            StartCoroutine(HurtAnimation());
            base.Damage(damage);
            UIManager.Instance.SetLifeUI(_currentHealth);
        }
    }

    /// <summary>
    /// instant death
    /// </summary>
    public void InstantDeath()
    {
        base.Damage(9999);
    }

    protected override void OnDeath()
    {
        GameManager.Instance.players.Remove(transform.GetComponent<PlayerControl>());

        if (!_playerControl.tutorial)
        {
            StateMachine.Instance.ChangeState(new GameOverState());
        }
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
            _currentUnattackTime += 0.2f;
            yield return new WaitForSeconds(0.2f);
        }
        for (int i = 0; i < mr.Length; i++)
        {
            mr[i].enabled = true;
        }
        _isUnattackable = false;
    }
}
