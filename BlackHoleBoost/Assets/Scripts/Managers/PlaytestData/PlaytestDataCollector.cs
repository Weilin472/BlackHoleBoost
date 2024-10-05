using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [10/04/2024]
 * [collects data for playtest]
 */

public class PlaytestDataCollector : Singleton<PlaytestDataCollector>
{
    //round data
    public int secondsSurvived;
    //need
    public float averageSpeed;
    public int numberOfBlackHoles;

    //shoot asteroid data
    //implemented
    public int totalAsteroidsCollected;
    public int normalAsteroidsCollected;
    public int bounceAsteroidsCollected;
    public int stickyAsteroidsCollected;

    //imp
    public int totalAsteroidShotsFired;
    public int normalAsteroidShotsFired;
    public int bounceAsteroidShotsFired;
    public int stickyAsteroidShotsFired;

    //imp
    public int asteroidsStuck;
    public int enemiesStuck;

    //imp
    public int numberOfShootAsteroidReachingBarrier;

    //enemy info
    //imp
    public int bigAsteroidSpawn;
    public int mediumAsteroidSpawn;
    public int numberOfAsteroidsCrash;

    //imp
    public float averageNuberOfEnemiesInScene;

    //Remember, for prototype spawner
    //imp
    public int numberOfEnemySpawns;
    public int cyclopsSpawned;
    public int minotaurSpawned;

    //imp
    public string[] playerHits;

    private bool _startCollecting = false;
    private float _collectFrequency = 1f;

    private List<float> _speeds;
    private List<int> _numEnemies;

    /// <summary>
    /// resets all values
    /// </summary>
    public void ResetValues()
    {
        secondsSurvived = 0;
        averageSpeed = 0;
        numberOfBlackHoles = 0;
        totalAsteroidsCollected = 0;
        normalAsteroidsCollected = 0;
        bounceAsteroidsCollected = 0;
        stickyAsteroidsCollected = 0;
        totalAsteroidShotsFired = 0;
        normalAsteroidShotsFired = 0;
        bounceAsteroidShotsFired = 0;
        stickyAsteroidShotsFired = 0;
        asteroidsStuck = 0;
        enemiesStuck = 0;
        numberOfShootAsteroidReachingBarrier = 0;
        bigAsteroidSpawn = 0;
        mediumAsteroidSpawn = 0;
        numberOfAsteroidsCrash = 0;
        averageNuberOfEnemiesInScene = 0;
        numberOfEnemySpawns = 0;
        cyclopsSpawned = 0;
        minotaurSpawned = 0;
        playerHits = new string[0];

        _speeds = new List<float>();
        _numEnemies = new List<int>();
    }

    /// <summary>
    /// adds a string of what hits the player
    /// </summary>
    /// <param name="enemy">what player was hit by</param>
    public void AddPlayerHit(string enemy)
    {
        string[] temp = (string[])playerHits.Clone();

        playerHits = new string[temp.Length + 1];

        for (int i = 0; i < temp.Length; i++)
        {
            playerHits[i] = temp[i];
        }

        playerHits[temp.Length] = enemy;
    }

    public void StartCollecting()
    {
        _startCollecting = true;
        StartCoroutine(Collect());
    }

    public void StopCollecting()
    {
        _startCollecting = false;
    }

    /// <summary>
    /// collects info for average data
    /// </summary>
    /// <returns></returns>
    private IEnumerator Collect()
    {
        while (_startCollecting)
        {
            Debug.Log("Buh");
            //get speeds
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null && player.GetComponent<Rigidbody>())
            {
                Rigidbody rigidbody = player.GetComponent<Rigidbody>();
                _speeds.Add(rigidbody.velocity.magnitude);

                if (_speeds.Count > 0)
                {
                    averageSpeed = _speeds.Average();
                }
            }

            _numEnemies.Add(GameObject.FindGameObjectsWithTag("Enemy").Length);
            if (_numEnemies.Count > 0)
            {
                float total = 0;
                foreach (int num in _numEnemies)
                {
                    total += (float)num;
                }
                averageNuberOfEnemiesInScene = total / _numEnemies.Count;
            }
            //get num of enemies
            yield return new WaitForSeconds(_collectFrequency);
        }
    }
}
