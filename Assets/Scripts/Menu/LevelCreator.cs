using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    [Header("Level Count")]
    [SerializeField] int levelCount;

    [Header("Asteroid Count")]
    [SerializeField] int minAsteroidCount;
    [SerializeField] int maxAsteroidCount;

    [Header("Asteroid Speed")]
    [SerializeField] float minAsteroidSpeed;
    [SerializeField] float maxAsteroidSpeed;

    [Header("Asteroid Spawn Delay")]
    [SerializeField] float minAsteroidSpawnDelay;
    [SerializeField] float maxAsteroidSpawnDelay;

    [Header("Points For Asteroid")]
    [SerializeField] int minPointsForAsteroid;
    [SerializeField] int maxPointsForAsteroid;


    void Start()
    {
        if (!SaveController.IsThisTheFirstLaunch())
            CreateLevels();
    }


    void CreateLevels()
    {
        GameParametrs gameParametrs = new GameParametrs();
        gameParametrs.levelCount = levelCount;
        SaveController.SaveGameParametrs(gameParametrs);


        LevelParametrs levelParmetrs = new LevelParametrs();

        for (int i = 0; i<levelCount; i++)
        {
            if (i == 0)
                levelParmetrs.open = true;
            else
                levelParmetrs.open = false;

            levelParmetrs.completed = false;

            levelParmetrs.asteroidsCount = Random.Range(minAsteroidCount, maxAsteroidCount);
            levelParmetrs.asteroidSpeed = Random.Range(minAsteroidSpeed, maxAsteroidSpeed);
            levelParmetrs.asteroidSpawnDelay = Random.Range(minAsteroidSpawnDelay, maxAsteroidSpawnDelay);
            levelParmetrs.pointsForAsteroid = Random.Range(minPointsForAsteroid, maxPointsForAsteroid);

            SaveController.SaveLevelParametrs(i, levelParmetrs);
        }
    }
}