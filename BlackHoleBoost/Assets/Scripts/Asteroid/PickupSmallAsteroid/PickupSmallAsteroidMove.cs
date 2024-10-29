using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [10/28/2024]
 * [movement script for pickup asteroids]
 */

public class PickupSmallAsteroidMove : AsteroidMove
{
    //variable for gravitate twoards
    [SerializeField] private float _magnetSpeed = 1f;
    [SerializeField] private float _magnetDistance = 1f;
    private bool _magnet = true;
    private GameObject[] players;

    /// <summary>
    /// calls to gravitate twoards players
    /// </summary>
    private void Update()
    {
        GravitateTwoardsPlayer();
    }

    /// <summary>
    /// if magnet is on, then the pick up gravitate twoards the closer player
    /// </summary>
    private void GravitateTwoardsPlayer()
    {
        if (_magnet)
        {
            players = GameObject.FindGameObjectsWithTag("Player");
            GameObject target = gameObject;
            float targetDistance = 99f;
            foreach (GameObject player in players)
            {
                float currentDistance = Vector3.Distance(player.transform.position, transform.position);
                if (currentDistance < _magnetDistance)
                {
                    if (target == gameObject || targetDistance > currentDistance)
                    {
                        target = player;
                        targetDistance = currentDistance;
                    }
                }
            }

            if (target != gameObject)
            {
                Vector3 direction = target.transform.position - transform.position;
                direction = direction.normalized;
                transform.position = transform.position + direction * _magnetSpeed * Time.deltaTime;
            }
        }
    }

    /// <summary>
    /// sets if the pickup gravitates twoards player
    /// </summary>
    public bool magnet
    {
        set { _magnet = value; }
    }
}
