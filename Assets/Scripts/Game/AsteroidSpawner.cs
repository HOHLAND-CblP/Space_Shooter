using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    float asteroidSpeed;
    [SerializeField] GameObject asteroidPref;

    int asteroidCount;
    float asteroidSpawnDelay;

    [Space]

    [SerializeField] GameLogic gameLogic;


    Camera mainCam;


    private void Start()
    {
        mainCam = Camera.main;

        asteroidSpeed = gameLogic.GetAsteroidSpeed();
        asteroidCount = gameLogic.GetAsteroidsCount();
        asteroidSpawnDelay = gameLogic.GetAsteroidSpawnDelay();
        

        StartCoroutine(SpawnDaley());
    }



    public void SpawnAsteroid()
    {
        float maxXPosition = mainCam.ScreenToWorldPoint(new Vector2(Screen.width*0.85f,0)).x;
        Vector2 spawnPosition = new Vector2(Random.Range(-maxXPosition, maxXPosition), transform.position.y);
        Asteroid ast = Instantiate(asteroidPref, spawnPosition, Quaternion.identity).GetComponent<Asteroid>();

        ast.Initialize(asteroidSpeed);
        ast.asteroidBlownUp.AddListener(AsteroidBlownUp);
        ast.asteroidDestroyed.AddListener(AsteroidDestroyed);
    }

    void AsteroidBlownUp()
    {
        gameLogic.AsteroidBlownUp();
    }

    void AsteroidDestroyed()
    {
        gameLogic.AsteroidDestroyed();
    }

    IEnumerator SpawnDaley()
    {
        SpawnAsteroid();
        asteroidCount--;

        if (asteroidCount!=0)
        {
            yield return new WaitForSeconds(asteroidSpawnDelay);

            StartCoroutine(SpawnDaley());
        }
    }
}