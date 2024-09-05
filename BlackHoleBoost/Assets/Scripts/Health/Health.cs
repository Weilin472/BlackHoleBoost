using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [09/05/2024]
 * [movement script for asteroids]
 */

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 3;
    private int _currentHealth;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void Damage(int damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            //on death object pool
            OnDeath();
        }
    }

    public void OnDeath()
    {
        Debug.Log("Death");
    }
}

