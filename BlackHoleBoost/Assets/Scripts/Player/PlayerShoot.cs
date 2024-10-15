using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [10/12/2024]
 * [script that handles shooting]
 */

public class PlayerShoot : MonoBehaviour
{
    private PlayerAsteroidInventory _playerAsteroidInventory;
    private ShootSmallAsteroidPool _shootSmallAsteroidPool;

    private SmallAsteroidType _currentAsteroid = SmallAsteroidType.NONE;
    private SmallAsteroidType[] _switchOrder;

    [SerializeField] private GameObject _currentAsteroidDisplay;
    private MeshRenderer _displayMeshRenderer;

    [SerializeField] private Material _displayNormal;
    [SerializeField] private Material _displayBounce;
    [SerializeField] private Material _displaySticky;

    [SerializeField] private float _fireRate = .5f;
    private bool _onCooldown = false;

    /// <summary>
    /// gets needed gameobjects
    /// </summary>
    private void OnEnable()
    {
        _playerAsteroidInventory = GetComponent<PlayerAsteroidInventory>();
        _shootSmallAsteroidPool = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ShootSmallAsteroidPool>();
        _displayMeshRenderer = _currentAsteroidDisplay.GetComponent<MeshRenderer>();

        _switchOrder = new SmallAsteroidType[3];
        _switchOrder[0] = SmallAsteroidType.NORMAL;
        _switchOrder[1] = SmallAsteroidType.BOUNCE;
        _switchOrder[2] = SmallAsteroidType.STICKY;
    }

    private void Update()
    {
        UpdateCurrentAsteroid();
        UpdateDisplayAsteroid();
    }

    /// <summary>
    /// shoots asteroid in front of player
    /// </summary>
    public void ShootAsteroid()
    {
        if (!_onCooldown)
        {
            SmallAsteroidType asteroid = _currentAsteroid;

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
                _playerAsteroidInventory.OnShootAsteroid(asteroid);

                Vector3 spawnLoc = transform.position;
                spawnLoc = spawnLoc + transform.up;

                Vector3 dir = spawnLoc - transform.position;
                dir.Normalize();

                _shootSmallAsteroidPool.Spawn(asteroid, spawnLoc, dir);
            }
        }
    }

    /// <summary>
    /// automatically switches current Asteroid to an asteroid the player has
    /// </summary>
    private void UpdateCurrentAsteroid()
    {
        if (_playerAsteroidInventory.HasAsteroidType(_currentAsteroid) && _currentAsteroid != SmallAsteroidType.NONE)
        {
            return;
        }
        else if (_playerAsteroidInventory.HasAsteroidType(SmallAsteroidType.NORMAL))
        {
            _currentAsteroid = SmallAsteroidType.NORMAL;
            return;
        }
        else if (_playerAsteroidInventory.HasAsteroidType(SmallAsteroidType.BOUNCE))
        {
            _currentAsteroid = SmallAsteroidType.BOUNCE;
            return;
        }
        else if (_playerAsteroidInventory.HasAsteroidType(SmallAsteroidType.STICKY))
        {
            _currentAsteroid = SmallAsteroidType.STICKY;
            return;
        }
        else
        {
            _currentAsteroid = SmallAsteroidType.NONE;
        }

    }

    /// <summary>
    /// updates the display of the current asteroid the player can shoot
    /// </summary>
    private void UpdateDisplayAsteroid()
    {
        switch (_currentAsteroid)
        {
            case SmallAsteroidType.NONE:
                _currentAsteroidDisplay.SetActive(false);
                break;
            case SmallAsteroidType.NORMAL:
                _currentAsteroidDisplay.SetActive(true);
                _displayMeshRenderer.material = _displayNormal;
                break;
            case SmallAsteroidType.BOUNCE:
                _currentAsteroidDisplay.SetActive(true);
                _displayMeshRenderer.material = _displayBounce;
                break;
            case SmallAsteroidType.STICKY:
                _currentAsteroidDisplay.SetActive(true);
                _displayMeshRenderer.material = _displaySticky;
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Manually switches the current asteroid to a different asteroid the player has
    /// </summary>
    public void SwitchCurrentAsteroid()
    {
        int currentCheck = (int)_currentAsteroid;
        for (int i = 0; i < 3; i++)
        {
            currentCheck++;
            if (currentCheck > 3)
            {
                currentCheck = 1;
            }

            if (_playerAsteroidInventory.HasAsteroidType((SmallAsteroidType)currentCheck))
            {
                _currentAsteroid = (SmallAsteroidType)currentCheck;
                return;
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
