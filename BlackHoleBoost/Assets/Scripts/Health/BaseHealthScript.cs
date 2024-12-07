using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [09/28/2024]
 * [Base Class for all health scripts]
 */

public class BaseHealthScript : MonoBehaviour
{
    protected int _currentHealth;
    [SerializeField] protected int _maxHealth;
    [SerializeField] protected AudioClip _damageSFX;

    protected bool _invincible = false;

    public int CurrentHealth => _currentHealth;

    /// <summary>
    /// called when looses all health
    /// </summary>
    protected virtual void OnDeath()
    {
        Debug.Log("using base version of health script, override OnDeath() if item is in object pool");
        Destroy(gameObject);
    }

    /// <summary>
    /// removes damage from healt
    /// </summary>
    /// <param name="damage">amount of health lost</param>
    public virtual void Damage(int damage)
    {
        if (!_invincible)
        {
            _currentHealth -= damage;

            if (_damageSFX != null)
            {
                AudioManager.Instance.PlaySFX(_damageSFX);
            }

            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
                OnDeath();
            }
        }
    }

    /// <summary>
    /// heals player
    /// </summary>
    /// <param name="healAmount">amount of health gained</param>
    public virtual void Heal(int healAmount)
    {
        _currentHealth += healAmount;
        if (_currentHealth>_maxHealth)
        {
            _currentHealth = _maxHealth;
        }

        UIManager.Instance.SetLifeUI(_currentHealth);
    }

    /// <summary>
    /// resets health
    /// </summary>
    protected virtual void ResetHealth()
    {
        _currentHealth = _maxHealth;
    }
}
