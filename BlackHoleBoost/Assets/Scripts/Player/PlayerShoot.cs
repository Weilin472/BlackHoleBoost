using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [10/03/2024]
 * [script that handles shooting]
 */

public class PlayerShoot : MonoBehaviour
{
    private PlayerAsteroidInventory _playerAsteroidInventory;
    private ShootSmallAsteroidPool _shootSmallAsteroidPool;

    [SerializeField] private float _fireRate = .5f;
    private bool _onCooldown = false;

    //
    private void OnEnable()
    {
        _playerAsteroidInventory = GetComponent<PlayerAsteroidInventory>();
        _shootSmallAsteroidPool = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ShootSmallAsteroidPool>();
    }

    /// <summary>
    /// shoots asteroid in front of player
    /// </summary>
    public void ShootAsteroid()
    {
        if (!_onCooldown)
        {
            SmallAsteroidType asteroid = _playerAsteroidInventory.PopNextShootAsteroid();

            if (asteroid != SmallAsteroidType.NONE)
            {
                //playtest data
                if (PlaytestDataCollector.Instance != null)
                {
                    PlaytestDataCollector.Instance.totalAsteroidShotsFired++;
                    switch (asteroid)
                    {
                        case SmallAsteroidType.NORMAL:
                            PlaytestDataCollector.Instance.normalAsteroidShotsFired++;
                            break;
                        case SmallAsteroidType.BOUNCE:
                            PlaytestDataCollector.Instance.bounceAsteroidShotsFired++;
                            break;
                        case SmallAsteroidType.STICKY:
                            PlaytestDataCollector.Instance.stickyAsteroidShotsFired++;
                            break;
                        default:
                            break;
                    }
                }

                StartCoroutine(Cooldown());

                Vector3 spawnLoc = transform.position;
                spawnLoc = spawnLoc + transform.up;

                Vector3 dir = spawnLoc - transform.position;
                dir.Normalize();

                _shootSmallAsteroidPool.Spawn(asteroid, spawnLoc, dir);
            }
        }
    }

    /// <summary>
    /// cooldown so player cant spam asteroids
    /// </summary>
    /// <returns></returns>
    private IEnumerator Cooldown()
    {
        _onCooldown = true;
        yield return new WaitForSeconds(_fireRate);
        _onCooldown = false;
    }
}
