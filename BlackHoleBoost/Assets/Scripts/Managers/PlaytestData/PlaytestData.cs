using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [10/04/2024]
 * [data on playtest]
 */

public class PlaytestData
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
    public float averageNuberOfEnemiesInScene;

    //Remember, for prototype spawner
    //imp
    public int numberOfEnemySpawns;
    public int cyclopsSpawned;
    public int minotaurSpawned;

    //need
    public string[] playerHits;

    public PlaytestData()
    {
        secondsSurvived = PlaytestDataCollector.Instance.secondsSurvived;
        averageSpeed = PlaytestDataCollector.Instance.averageSpeed;
        numberOfBlackHoles = PlaytestDataCollector.Instance.numberOfBlackHoles;
        totalAsteroidsCollected = PlaytestDataCollector.Instance.totalAsteroidsCollected;
        normalAsteroidsCollected = PlaytestDataCollector.Instance.normalAsteroidsCollected;
        bounceAsteroidsCollected = PlaytestDataCollector.Instance.bounceAsteroidsCollected;
        stickyAsteroidsCollected = PlaytestDataCollector.Instance.stickyAsteroidsCollected;
        totalAsteroidShotsFired = PlaytestDataCollector.Instance.totalAsteroidsCollected;
        normalAsteroidShotsFired = PlaytestDataCollector.Instance.normalAsteroidsCollected;
        bounceAsteroidShotsFired = PlaytestDataCollector.Instance.bounceAsteroidsCollected;
        stickyAsteroidShotsFired = PlaytestDataCollector.Instance.stickyAsteroidsCollected;
        asteroidsStuck = PlaytestDataCollector.Instance.asteroidsStuck;
        enemiesStuck = PlaytestDataCollector.Instance.enemiesStuck;
        numberOfShootAsteroidReachingBarrier = PlaytestDataCollector.Instance.numberOfShootAsteroidReachingBarrier;
        bigAsteroidSpawn = PlaytestDataCollector.Instance.bigAsteroidSpawn;
        mediumAsteroidSpawn = PlaytestDataCollector.Instance.mediumAsteroidSpawn;
        numberOfAsteroidsCrash = PlaytestDataCollector.Instance.numberOfAsteroidsCrash;
        averageNuberOfEnemiesInScene = PlaytestDataCollector.Instance.averageNuberOfEnemiesInScene;
        numberOfEnemySpawns = PlaytestDataCollector.Instance.numberOfEnemySpawns;
        cyclopsSpawned = PlaytestDataCollector.Instance.cyclopsSpawned;
        minotaurSpawned = PlaytestDataCollector.Instance.minotaurSpawned;
        //playerHits = PlaytestDataCollector.Instance.playerHits.Clone();
    }
}
