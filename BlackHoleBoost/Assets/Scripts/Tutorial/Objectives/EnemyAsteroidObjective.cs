using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [11/03/2024]
 * [triggers dialogue when player destroys all asteroids or runs out of ammo]
 */

public class EnemyAsteroidObjective : DialogueTrigger
{
    private EnemyAsteroidPool _enemyAsteroidPool;
    private PickupSmallAsteroidPool _pickupSmallAsteroidPool;
    private List<EnemyAsteroid> _objectives;
    private GameObject _pickupSmallAsteroid;
    private bool _startCheck = false;

    private bool _respawningPickup = false;

    /// <summary>
    /// gets needed components and spawns 
    /// </summary>
    private void OnEnable()
    {
        _objectives = new List<EnemyAsteroid>();
        _enemyAsteroidPool = GameManager.Instance.gameObject.GetComponent<EnemyAsteroidPool>();
        _pickupSmallAsteroidPool = GameManager.Instance.gameObject.GetComponent<PickupSmallAsteroidPool>();

        _objectives.Add(_enemyAsteroidPool.TutorialSpawn(new Vector3(1, 3, 0)));
        _objectives.Add(_enemyAsteroidPool.TutorialSpawn(new Vector3(4, -3, 0)));
        _objectives.Add(_enemyAsteroidPool.TutorialSpawn(new Vector3(-3, -2, 0)));

        _pickupSmallAsteroid = _pickupSmallAsteroidPool.TutorialSpawn(new Vector3(0, 2, 0), SmallAsteroidType.NORMAL).gameObject;


        _startCheck = true;
    }

    /// <summary>
    /// checks if all asteroids have been picked up
    /// </summary>
    private void Update()
    {
        if (_startCheck)
        {
            if (!_pickupSmallAsteroid.activeSelf && !_respawningPickup)
            {
                _respawningPickup = true;
                StartCoroutine(RespawnPickup());
            }
            foreach (EnemyAsteroid asteroid in _objectives)
            {
                if (asteroid.gameObject.activeSelf)
                {
                    return;
                }
            }
            TriggerDialogue();
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// respawns pickup asteroids
    /// </summary>
    /// <returns></returns>
    private IEnumerator RespawnPickup()
    {
        yield return new WaitForSeconds(2);
        _pickupSmallAsteroid = _pickupSmallAsteroidPool.TutorialSpawn(new Vector3(0, 2, 0), SmallAsteroidType.NORMAL).gameObject;
        _respawningPickup = false;
    }
}
