using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [10/04/2024]
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
        if ( !_isUnattackable&&!transform.GetComponent<PlayerControl>().isInBlackHole)
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
        _playerControl.ExitBlackHoleMode();
        PrototypeGameManager.Instance.GameOver();
    //    StateMachine.Instance.GameEnd();
        GameManager.Instance.players.Remove(transform.GetComponent<PlayerControl>());
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
