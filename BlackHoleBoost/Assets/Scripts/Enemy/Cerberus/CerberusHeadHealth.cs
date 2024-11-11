using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [11/10/2024]
 * [health for each head of cerberus (not inherited since we need to do things in on trigger enter)]
 */

public class CerberusHeadHealth : MonoBehaviour
{
    [SerializeField] private SmallAsteroidType _damageableType;
    private Cerberus _cerberus;

    private void OnEnable()
    {
        _cerberus = transform.root.gameObject.GetComponent<Cerberus>();
    }

    /// <summary>
    /// checks if it's an asteroid and if it is the right type of asteroid
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.gameObject.TryGetComponent<ShootSmallAsteroidType>(out ShootSmallAsteroidType asteroidType))
        {
            if (asteroidType.GetAsteroidType() == _damageableType)
            {
                _cerberus.AttackPersuit();
                gameObject.SetActive(false);
            }
        }
    }
}
