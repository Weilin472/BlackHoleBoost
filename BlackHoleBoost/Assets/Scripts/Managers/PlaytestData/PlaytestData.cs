using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [10/02/2024]
 * [data on playtest]
 */

public class PlaytestData : Singleton<PlaytestData>
{
    //round data
    public int secondsSurvived;
    public float averageSpeed;
    public int numberOfBlackHoles;

    //shoot asteroid data
    public int totalAsteroidsCollected;
    public int normalAsteroidsCollected;
    public int bounceAsteroidsCollected;
    public int stickyAsteroidCollected;

    public int totalAsteroidShotsFired;
    public int normalAsteroidShotsFired;
    public int bounceAsteroidShotsFired;
    public int stickyAsteroidShotsFired;

    public int asteroidsStuck;
    public int enemiesStuck;

    public int numberOfAsteroidReachingBarrier;
    public int totalNumberOfAsteroidsHitBarrier;

    //enemy info
    public int bigAsteroidSpawn;
    public int smallAsteroidSpawn;
    public int numberOfAsteroidsCrash;

    public int numberOfEnemySpawns;
    public int cyclopsSpawned;
    public int minotaurSpawned;

    public string[] playerHits;

    private void ResetValues()
    {
        secondsSurvived = 0;
        averageSpeed = 0;
        numberOfBlackHoles = 0;
        totalAsteroidsCollected = 0;
        normalAsteroidsCollected = 0;
        bounceAsteroidsCollected = 0;
        stickyAsteroidCollected = 0;
        totalAsteroidShotsFired = 0;
        normalAsteroidShotsFired = 0;
        bounceAsteroidShotsFired = 0;
        stickyAsteroidShotsFired = 0;
        asteroidsStuck = 0;
        enemiesStuck = 0;
        numberOfAsteroidReachingBarrier = 0;
        totalNumberOfAsteroidsHitBarrier = 0;
        bigAsteroidSpawn = 0;
        smallAsteroidSpawn = 0;
        numberOfAsteroidsCrash = 0;
        numberOfEnemySpawns = 0;
        cyclopsSpawned = 0;
        minotaurSpawned = 0;
        playerHits = new string[0];
    }
}
