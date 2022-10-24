using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
    LevelParametrs levelParametrs;
    int asteroidsLeft;



    int score;

    

    // Events
    public UnityEvent<bool> gamePaused = new UnityEvent<bool>();
    public UnityEvent<int> scoreUpdate = new UnityEvent<int>();
    public UnityEvent gameComplete = new UnityEvent();

    void Start()
    {
        LoadLevelParametrs();
        asteroidsLeft = levelParametrs.asteroidsCount;
    }

    public int GetAsteroidsCount() { return levelParametrs.asteroidsCount; }
    public float GetAsteroidSpawnDelay() { return levelParametrs.asteroidSpawnDelay; }
    public float GetAsteroidSpeed() { return levelParametrs.asteroidSpeed; }


    public void AddPointsToScore(int points)
    {
        score += points;

        scoreUpdate.Invoke(score);
    }


    public void AsteroidBlownUp()
    {
        asteroidsLeft--;
        AddPointsToScore(levelParametrs.pointsForAsteroid);

        if (asteroidsLeft == 0)
        {
            gameComplete.Invoke();
        }
    }

    public void AsteroidDestroyed()
    {
        asteroidsLeft--;

        if (asteroidsLeft == 0)
        {
            gameComplete.Invoke();
        }
    }

    void LoadLevelParametrs()
    {
        levelParametrs = SaveController.LoadLevelParametrs(SaveController.curLoadLevel);
    }

    void SaveLevelParametrs()
    {
        SaveController.SaveLevelParametrs(SaveController.curLoadLevel, levelParametrs);
    }


    public void GameOver()
    {
        Time.timeScale = 0;
    }
    
    public void GameComplete()
    {
        levelParametrs.completed = true;

        

        LevelParametrs nextLevelParams = SaveController.LoadLevelParametrs(SaveController.curLoadLevel+1);
        if(nextLevelParams!=null)
        {
            nextLevelParams.open = true;

            SaveController.SaveLevelParametrs(SaveController.curLoadLevel+1, nextLevelParams);
        }    


        SaveLevelParametrs();
    }

    public void GamePause(bool isPaused)
    {
        switch(isPaused)
        {
            case true:
                Time.timeScale = 0;
                break;

            case false:
                Time.timeScale = 1;
                break;
        }
    }
}