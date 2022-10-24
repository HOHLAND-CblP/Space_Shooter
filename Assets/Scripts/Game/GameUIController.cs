using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;


public class GameUIController : MonoBehaviour
{
    [Header("Star Ship")]
    [SerializeField] StarShip starShip;

    [Header("Game Logic")]
    [SerializeField] GameLogic gameLogic;
    
    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;

    [Header("Game End Panel")]
    [SerializeField] GameObject gameEndPanel;
    [SerializeField] TextMeshProUGUI headerGameEndPanel;
    [SerializeField] TextMeshProUGUI finalScore;


    [Header("StarShip Health")]
    [SerializeField] GameObject[] healthImages;



    private void Start()
    {
        gameLogic.scoreUpdate.AddListener(ScoreUpdate);
        gameLogic.gameComplete.AddListener(GameComplete);

        starShip.damage.AddListener(StarShipDamage);
        starShip.gameOver.AddListener(GameOver);
    }

    public void GamePause(bool isGamePaused)
    {
        gameLogic.GamePause(isGamePaused);
    } 




    void ScoreUpdate(int score)
    {
        scoreText.text = score.ToString();
        finalScore.text = "You Score: " + score.ToString();
    }

    void StarShipDamage(int health)
    {
        healthImages[health].SetActive(false);
    }

    void GameOver()
    {
        gameEndPanel.SetActive(true);
        headerGameEndPanel.text = "Game Over";

        gameLogic.GameOver();
    }

    void GameComplete()
    {
        gameEndPanel.SetActive(true);
        headerGameEndPanel.text = "Game Complete";

        gameLogic.GameComplete();
    }
}