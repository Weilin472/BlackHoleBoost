using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [09/22/2024]
 * [script that handles shooting]
 */

public class PlayerShoot : MonoBehaviour
{
    private PlayerAsteroidInventory _playerAsteroidInventory;
    private ShootSmallAsteroidPool _shootSmallAsteroidPool;

    [SerializeField] private float _fireRate = .5f;
    private bool _onCooldown = false;

    private void OnEnable()
    {
        _playerAsteroidInventory = GetComponent<PlayerAsteroidInventory>();
        _shootSmallAsteroidPool = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ShootSmallAsteroidPool>();
    }

    public void ShootAsteroid()
    {
        if (!_onCooldown)
        {
            SmallAsteroidType asteroid = _playerAsteroidInventory.PopNextShootAsteroid();

            if (asteroid != SmallAsteroidType.NONE)
            {
                StartCoroutine(Cooldown());

                Vector3 spawnLoc = transform.position;
                spawnLoc.y = spawnLoc.y + 1;

                Vector3 dir = spawnLoc - transform.position;
                dir.Normalize();

                _shootSmallAsteroidPool.Spawn(asteroid, spawnLoc, dir);
            }
        }
    }

    private IEnumerator Cooldown()
    {
        _onCooldown = true;
        yield return new WaitForSeconds(_fireRate);
        _onCooldown = false;
    }
}
