using System.Collections;
using System.Collections.Generic;
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

    //need
    public int averageNuberOfEnemiesInScene;

    //Remember, for prototype spawner
    //imp
    public int numberOfEnemySpawns;
    public int cyclopsSpawned;
    public int minotaurSpawned;

    //need
    public string[] playerHits;

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
    }
}
