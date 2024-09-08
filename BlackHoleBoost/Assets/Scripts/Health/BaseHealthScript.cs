using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [09/07/2024]
 * [Base Class for all health scripts]
 */

public class BaseHealthScript : MonoBehaviour
{
    protected int _currentHealth;
    [SerializeField] protected int _maxHealth;

    protected virtual void OnDeath()
    {
        Debug.LogError("using base version of health script, override OnDeath()");
    }

    public virtual void Damage(int damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            OnDeath();
        }
    }

    protected virtual void ResetHealth()
    {
        _currentHealth = _maxHealth;
    }
}
