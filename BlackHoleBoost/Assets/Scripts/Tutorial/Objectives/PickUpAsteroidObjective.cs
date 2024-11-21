using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [11/03/2024]
 * [triggers dialogue when player collects all asteroids]
 */

public class PickUpAsteroidObjective : DialogueTrigger
{
    private PickupSmallAsteroidPool _pickupSmallAsteroidPool;
    private List<PickupSmallAsteroid> _objectives;
    private bool _startCheck = false;

    /// <summary>
    /// gets needed components and spawns 
    /// </summary>
    private void OnEnable()
    {
        _objectives = new List<PickupSmallAsteroid>();
        _pickupSmallAsteroidPool = GameManager.Instance.gameObject.GetComponent<PickupSmallAsteroidPool>();

        _objectives.Add(_pickupSmallAsteroidPool.TutorialSpawn(Vector3.zero, SmallAsteroidType.STICKY));
        _objectives.Add(_pickupSmallAsteroidPool.TutorialSpawn(new Vector3(4, 3, 0), SmallAsteroidType.NORMAL));
        _objectives.Add(_pickupSmallAsteroidPool.TutorialSpawn(new Vector3(-3, -2, 0), SmallAsteroidType.NORMAL));
        _objectives.Add(_pickupSmallAsteroidPool.TutorialSpawn(new Vector3(3, -3, 0), SmallAsteroidType.BOUNCE));
        _objectives.Add(_pickupSmallAsteroidPool.TutorialSpawn(new Vector3(-3, 4, 0), SmallAsteroidType.BOUNCE));

        _startCheck = true;
    }

    /// <summary>
    /// checks if all asteroids have been picked up
    /// </summary>
    private void Update()
    {
        if (_startCheck)
        {
            foreach (PickupSmallAsteroid asteroid in _objectives)
            {
                if (asteroid.gameObject.activeSelf)
                {
                    Debug.Log("Check");
                    return;
                }
            }
            TriggerDialogue();
            gameObject.SetActive(false);
        }
    }
}
